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
    public static class AddMethods
    {
        public static async Task Add01_simple()
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



        public static async Task Add02_RelatedObjects()
        {
            // tworzymy nowe obiekty typu user, tag, category
            // następnie używamy tych obiektów przy tworzeniu nowego artykułu
            // dodanie artykułu powoduje wcześniejsze dodanie do bazy
            // uzytkownika, tagu, kategorii
            // ważne aby operować na referencjach nie na numerach id
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


        public static async Task Add03_RelatedObjects()
        {
          // tworzymy nowy artykuł
          // używamy istniejące obiekty typu user, tag, category
          // zapisujemy numery id, nie musimy operować na referencjach

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

       
                context.Posts.Add(post);
                context.PostTags.AddRange(postTags);

                DisplayEntitiesInfo(context);
                await context.SaveChangesAsync();

                Console.WriteLine(post.Id);


            }

        }



        public static async Task Add05_AddvsAddRange()
        {
            // Dodajemy 3000 nowych tagów
            // Następnie zapisujemy je do bazy na dwa sposoby
            // za pomocą Add() uruchomionego w pętli
            // za pomocą AddRange()
            // porównujemy czas wykonania obu metod

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

        
        
        public static async Task Add06_WithBulkInsert()
        {

            // Test z wykorzystaniem BulkExtensions
            // Dodajemy 3000 nowych tagów
            // Następnie zapisujemy je do bazy za pomocą BulkInsert

            var tags = new List<Tag>();
            for (int i = 0; i < 3000; i++)
            {
                tags.Add(new Tag { Name = $"BName {i}", Url = $"BUrl {i}" });
            }


            var stopwatch = new Stopwatch();


            using (var context = new ApplicationDbContext())
            {

                stopwatch.Start();

                // Uwaga context.SaveChangesAsync() nie jest potrzebne
                await context.BulkInsertAsync(tags);


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
