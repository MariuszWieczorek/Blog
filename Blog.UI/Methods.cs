using System;
using System.Collections.Generic;
using System.Linq;
using Blog.DataLayer;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;
using Blog.Domain.Entities;
using System.Diagnostics;

namespace Blog.UI
{
    public static class Methods
    {
        
        public static async Task ExplicitLoading01()
        {
            // pobieramy pojedynczą właściwość
            using (var context = new ApplicationDbContext())
            {

                var posts = await context.Posts
                    .FirstOrDefaultAsync();

                context.Entry(posts).Reference(x => x.User).Load();

            }
        }
        
        public static async Task ExplicitLoading02()
        {

           

            using (var context = new ApplicationDbContext())
            {
                //pobieramy dodatkowo kolekcję
                var posts = await context.Posts
                    .FirstOrDefaultAsync();

                context.Entry(posts).Reference(x => x.User).Load();
                context.Entry(posts).Collection(x => x.Tags).Load();

            }
        }
        public static async Task ExplicitLoading03()
        {
            using (var context = new ApplicationDbContext())
            {
                // filtrujemy pobraną kolekcję
                var posts = await context.Posts
                    .FirstOrDefaultAsync();

                context.Entry(posts).Reference(x => x.User).Load();

                context.Entry(posts)
                    .Collection(x => x.Tags)
                    .Query()
                    .Where(x => x.Id > 3)
                    .Load();

            }
        }

        public static async Task IQueryableVsIEnumerable01()
        {
            using (var context = new ApplicationDbContext())
            {

                // domyślnie zwraca IQueryable<Post>
                var posts1a = context.Posts.Where(x => x.Id > 3);
                IQueryable<Post> posts1b = context.Posts.Where(x => x.Id > 3);

                // zostanie zwrócony IEnumerable<Post>
                var posts2a = context.Posts.Where(x => x.Id > 3).AsEnumerable();
                IEnumerable<Post> posts2b = context.Posts.Where(x => x.Id > 3).AsEnumerable();

            }
        }

        public static async Task IQueryableVsIEnumerable02()
        {
            using (var context = new ApplicationDbContext())
            {


                // najpierw zostanie wykonane zapytanie na bazie danych
                var posts = context.Posts.Where(x => x.Id > 3).AsEnumerable();


                // później w pamięci na już pobranej kolekcji zostaną wykonane dodatkowe filtrowania
                posts = posts.Where(x => x.CategoryId > 5);
                posts = posts.Take(2);

                foreach (var item in posts)
                    Console.WriteLine(item.Id);
                

            }
        }

        public static async Task IQueryableVsIEnumerable03()
        {
            using (var context = new ApplicationDbContext())
            {

                // poniższy kod zadziała tak jak ten powyżej
                // najpierw zostanie wykonane zapytanie na bazie danych
                var posts = context.Posts.Where(x => x.Id > 3).ToList();


                // później w pamięci zostaną wykonane dodatkowe filtrowania
                posts = posts.Where(x => x.CategoryId > 5).ToList();
                posts = posts.Take(2).ToList();

                foreach (var item in posts)
                    Console.WriteLine(item.Id);


            }
        }

        public static async Task IQueryableVsIEnumerable04()
        {
            using (var context = new ApplicationDbContext())
            {

                
                // na bazie danych zostanie wykonane całe zapytanie
                var posts = context.Posts.Where(x => x.Id > 3).AsQueryable();


            
                posts = posts.Where(x => x.CategoryId > 5);
                posts = posts.Take(2);

                foreach (var item in posts)
                    Console.WriteLine(item.Id);


            }
        }

        public static async Task ChangeTracker01()
        {
            var stopwatch = new Stopwatch();

            using (var context = new ApplicationDbContext())
            {



                stopwatch.Start();

                context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

                // na bazie danych zostanie wykonane całe zapytanie
                var posts = await context.Posts
                    .Include(x => x.ApprovedBy)
                    .Include(x => x.User)
                    .Include(x => x.Tags)
                    .Include(x => x.Category)
                    // .AsNoTracking()
                    .ToListAsync();

                stopwatch.Stop();

                DisplayEntitiesInfo(context);

                var elapsedTime = stopwatch.ElapsedMilliseconds;
                Console.WriteLine($"czas: {elapsedTime} ms");

            }
        }


        public static async Task Inne01()
        {
            var stopwatch = new Stopwatch();

            using (var context = new ApplicationDbContext())
            {


                stopwatch.Start();

                var posts = (await context.Posts
                    .AsNoTracking()
                    .ToListAsync())
                    .Where(x=>IsActualArticle(x.PostedOn));

                stopwatch.Stop();


                var elapsedTime = stopwatch.ElapsedMilliseconds;
                Console.WriteLine($"czas: {elapsedTime} ms");

            }
        }


        public static async Task RawSQL01()
        {
            

            using (var context = new ApplicationDbContext())
            {




                var posts1 = await context.Posts
                    .FromSqlRaw("SELECT * FROM POSTS2 WHERE ID = 1")
                    .ToListAsync();


                var title = "Title 7";

                var posts2 = await context.Posts
                    .FromSqlRaw("SELECT * FROM POSTS2 WHERE TITLE2 = {0}",title)
                    .ToListAsync();


                var posts3 = await context.Posts
                    .FromSqlInterpolated($"SELECT * FROM POSTS2 WHERE TITLE2 = {title}")
                    .ToListAsync();


                foreach (var item in posts2)
                {
                    Console.WriteLine(item.Id);
                }

            
            

            }
        }

        public static bool IsActualArticle(DateTime date) => date.Year >= DateTime.Now.Year;
        private static void DisplayEntitiesInfo(ApplicationDbContext context)
        {
            Console.WriteLine("---------------------");
            foreach (var item in context.ChangeTracker.Entries())
            {
                Console.WriteLine($"encja {item.Entity.GetType().Name}, Stan {item.State}");
            }
            Console.WriteLine("---------------------");
        }
    }
}
