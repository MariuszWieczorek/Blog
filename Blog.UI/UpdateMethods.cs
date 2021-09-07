using Blog.DataLayer;
using Blog.DataLayer.Extensions;
using Blog.Domain.Entities;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.UI
{
    public static class UpdateMethods
    {
        public static async Task Update01_simple()
        {
            // prosty update
            using (var context = new ApplicationDbContext())
            {

                var categoryToUpdate = await context.Categories.FindAsync(5);
                categoryToUpdate.Description = "x1";
                await context.SaveChangesAsync();
            }
        }


        public static async Task Update02_ExistedUser()
        {
            // wybieramy istniejącego użytkownika
            // i Id tego użytkownika zapisujemy w tabeli postów
            var stopwatch = new Stopwatch();

            Category categoryToUpdate = null;

            using (var context = new ApplicationDbContext())
            {

                categoryToUpdate = await context.Categories.FindAsync(5);
                categoryToUpdate.Description = "xs";
            }

            using (var context = new ApplicationDbContext())
            {
                context.Categories.Update(categoryToUpdate);
                context.Categories.Attach(categoryToUpdate).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }

        }


        public static async Task Update03_NewUser()
        {
            // aktualizujemy dodając nowego użytkownika
            // zamiast Id użytkownika podajemy referencję do użytkownika
            var stopwatch = new Stopwatch();

            var contact = new ContactInfo()
            {
                Email = "user123@onet.pl"
            };
            var User = new User()
            {
                Login = "User123",
                Password = "Pass123"
            };

            using (var context = new ApplicationDbContext())
            {

                var postToUpdate = await context.Posts.FindAsync(5);
                postToUpdate.ApprovedBy = User;
                await context.SaveChangesAsync();
            }

        }


        public static async Task Update04_ManyToMany()
        {
            // aktualizowanie powiązań wiele do wielu
            // za pomocą klasy rozszerzającej DbContext

            var stopwatch = new Stopwatch();

            var contact = new ContactInfo()
            {
                Email = "user123@onet.pl"
            };
            var User = new User()
            {
                Login = "User123",
                Password = "Pass123"
            };

            var newTags = new List<Tag>()
            {
                new Tag {Id = 1},
                new Tag {Id = 2},
                new Tag {Id = 3},
                new Tag {Id = 4},
                new Tag {Id = 5},
            };


            using (var context = new ApplicationDbContext())
            {

                var postToUpdate = await context.Posts.FindAsync(5);
                var postTags = await context.PostTags
                    .Where(x => x.PostId == postToUpdate.Id)
                    .AsNoTracking()
                    .ToListAsync();

                context.TryUpdateManyToMany(postTags,
                    newTags.Select(x=> new PostTag { TagId = x.Id, PostId = postToUpdate.Id } ),
                    x=>x.TagId
                    );


                DisplayEntitiesInfo(context);
                await context.SaveChangesAsync();
            }



        }


        public static async Task Update05_UpdateWithoutBulkUpdate()
        {
            var stopwatch = new Stopwatch();

          
            using (var context = new ApplicationDbContext())
            {

                stopwatch.Start();

                // nie możemy ustawić jako .AsNoTracking()
                // ponieważ żadna zmiana by sięnie wykonała
                var tagsToUpdate = await context.Tags
                    .ToListAsync();
                
                
                foreach (var item in tagsToUpdate)
                {
                    item.Name += '1';
                }

                //15414 ms
                await context.SaveChangesAsync();

                stopwatch.Stop();
                Console.WriteLine(stopwatch.ElapsedMilliseconds);
            }

        }

        public static async Task Update06_UpdateWithBulkUpdate()
        {
            var stopwatch = new Stopwatch();


            using (var context = new ApplicationDbContext())
            {

                stopwatch.Start();

                // tu możemy ustawić jako .AsNoTracking()
                var tagsToUpdate = await context.Tags
                    .AsNoTracking()
                    .ToListAsync();


                foreach (var item in tagsToUpdate)
                {
                    item.Name += '2';
                }

                // 4429ms
                // await context.SaveChangesAsync();
                await context.BulkUpdateAsync(tagsToUpdate);

                stopwatch.Stop();
                Console.WriteLine(stopwatch.ElapsedMilliseconds);
            }

        }

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
