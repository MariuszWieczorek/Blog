using Blog.DataLayer;
using Blog.DataLayer.Extensions;
using Blog.Domain.Entities;
using Blog.Domain.Enums;
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
    public static class Methods02
    {
        public static async Task Task01()
        {
            var category = new Category()
            { 
                Name= "MyEFCoreCategory4",
                Description = "Desc EFCore Category",
                Url = "ef-core"

            };

            using (var context = new ApplicationDbContext())
            {
                // zalecane uzywanie metody synchronicznej
                // raczej używamy Add() niż AddAsync()
                context.Categories.Add(category);
                
                DisplayEntitiesInfo(context);
                await context.SaveChangesAsync();

                DisplayEntitiesInfo(context);
                Console.WriteLine(category.Id);
            }

        }



        public static async Task Task02()
        {
            var category = new Category()
            {
                Name = "Category XYZ",
                Description = "Desc EFCore Category",
                Url = "ef-core"

            };

            var contactInfo = new ContactInfo()
            {
                Email = "mar72@op.pl"

            };

            var user = new User()
            {
                Login = "Mar72",
                Password = "pass",
                ContactInfo = contactInfo
            };

            var post = new Post()
            {
                ApprovedBy = user,
                User = user,
                Category = category,
                ImageUrl = "img",
                Description = "Desc1",
                ShortDescription = "desc",
                Modified = new DateTime(2021,08,01),
                PostedOn = new DateTime(2021,07,01),
                Published = true,
                Title = "New Title EF Core",
                Type = PostType.Plain,
                Url = "ur",
                Tags = new List<Tag>()
                {
                    new Tag(){Name = "Tag12",Url="tag12"}
                }
            };

            using (var context = new ApplicationDbContext())
            {

                // dodając sam post wszystkie powyższe zmiany zostaną dodane
                context.Posts.Add(post);
                DisplayEntitiesInfo(context);
                await context.SaveChangesAsync();

                Console.WriteLine(post.Id);
                Console.WriteLine(post.UserId);
                Console.WriteLine(post.ApprovedByUserId);
                Console.WriteLine(post.CategoryId);
                Console.WriteLine(post.Tags.First().Id);

            }

        }


        public static async Task Task03()
        {
          

            var post = new Post()
            {
                ApprovedByUserId = 4,
                UserId = 4,
                CategoryId = 9,
                ImageUrl = "img2",
                Description = "Desc22",
                ShortDescription = "desca",
                Modified = new DateTime(2021, 08, 01),
                PostedOn = new DateTime(2021, 07, 01),
                Published = true,
                Title = "New Title EF Core 22",
                Type = PostType.Plain,
                Url = "urls",

            };


               var postTags = new List<PostTag>()
                {
                    new PostTag {TagId = 1,Post = post}
                };

            using (var context = new ApplicationDbContext())
            {

                // dodając sam post wszystkie powyższe zmiany zostaną dodane
                context.Posts.Add(post);
                context.PostTags.AddRange(postTags);

                DisplayEntitiesInfo(context);
                await context.SaveChangesAsync();

                Console.WriteLine(post.Id);


            }

        }



        public static async Task Task04()
        {


            var post = new Post()
            {
                ApprovedByUserId = 4,
                UserId = 4,
                CategoryId = 9,
                ImageUrl = "img3",
                Description = "Desc32",
                ShortDescription = "desca3",
                Modified = new DateTime(2021, 08, 01),
                PostedOn = new DateTime(2021, 07, 01),
                Published = true,
                Title = "New Title EF Core 23",
                Type = PostType.Plain,
                Url = "url33",

            };


            var postTags = new List<PostTag>()
                {
                    new PostTag {TagId = 1,Post = post},
                    new PostTag {TagId = 2,Post = post},
                    new PostTag {TagId = 3,Post = post}
                };

            using (var context = new ApplicationDbContext())
            {

                // dodając sam post wszystkie powyższe zmiany zostaną dodane
                context.Posts.Add(post);
                context.PostTags.AddRange(postTags);

                DisplayEntitiesInfo(context);
                await context.SaveChangesAsync();

                Console.WriteLine(post.Id);


            }

        }


        // Dodajemy 3000 Tagów
        // Test z AddRange
        public static async Task Task05()
        {


            var tags = new List<Tag>();
            for (int i = 0; i < 3000; i++)
            {
                tags.Add(new Tag { Name = $"ZName {i}", Url =$"ZUrl {i}"});
            }


            var stopwatch = new Stopwatch();
            

            using (var context = new ApplicationDbContext())
            {

                stopwatch.Start();


                foreach (var item in tags)
                {
                    context.Tags.Add(item);
                }



                context.AddRange(tags);

                
                await context.SaveChangesAsync();
                stopwatch.Stop();
                Console.WriteLine(stopwatch.ElapsedMilliseconds);


            }

        }

        // Test z wykorzystaniem BulkExtensions
        // Uwaga context.SaveChangesAsync() nie jest potrzebne
        public static async Task Task06()
        {


            var tags = new List<Tag>();
            for (int i = 0; i < 3000; i++)
            {
                tags.Add(new Tag { Name = $"BName {i}", Url = $"BUrl {i}" });
            }


            var stopwatch = new Stopwatch();


            using (var context = new ApplicationDbContext())
            {

                stopwatch.Start();

                await context.BulkInsertAsync(tags);


                stopwatch.Stop();
                Console.WriteLine(stopwatch.ElapsedMilliseconds);


            }

        }

        public static async Task Update01()
        {
            // prosty update
            using (var context = new ApplicationDbContext())
            {

                var categoryToUpdate = await context.Categories.FindAsync(5);
                categoryToUpdate.Description = "x1";
                await context.SaveChangesAsync();
            }
        }


        public static async Task Update02()
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


        public static async Task Update03_ObiektyPowiazane()
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
