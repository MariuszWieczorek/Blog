using Blog.DataLayer;
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
    public static class DeleteMethods
    {
        public static async Task Delete01_Simple()
        {
            var stopwatch = new Stopwatch();


            using (var context = new ApplicationDbContext())
            {

                stopwatch.Start();

                // tu możemy ustawić jako .AsNoTracking()
                var categoryToDelete = await context.Categories
                    .FindAsync(5);


                context.Categories.Remove(categoryToDelete);

                DisplayEntitiesInfo(context);


                await context.SaveChangesAsync();


                stopwatch.Stop();
                Console.WriteLine(stopwatch.ElapsedMilliseconds);
            }

        }


        public static async Task Delete02_Simple()
        {
            var stopwatch = new Stopwatch();


            using (var context = new ApplicationDbContext())
            {

                stopwatch.Start();



                context.Categories.Remove(new Category { Id = 6 });

                DisplayEntitiesInfo(context);


                await context.SaveChangesAsync();


                stopwatch.Stop();
                Console.WriteLine(stopwatch.ElapsedMilliseconds);
            }

        }

        public static async Task Delete03_Simple()
        {
            var stopwatch = new Stopwatch();


            using (var context = new ApplicationDbContext())
            {

                stopwatch.Start();



                context.Categories.Remove(new Category { Id = 4 });

                DisplayEntitiesInfo(context);


                await context.SaveChangesAsync();


                stopwatch.Stop();
                Console.WriteLine(stopwatch.ElapsedMilliseconds);
            }

        }



        public static async Task Delete04_BulkDelete()
        {
            var stopwatch = new Stopwatch();


            using (var context = new ApplicationDbContext())
            {

                stopwatch.Start();

                var category = new Category() { Id = 4 };

                var postsToDelete = context.Posts
                    .Where(x => x.CategoryId == category.Id)
                    .ToListAsync();


                // usuwamy najpierw powiązane posty
                context.RemoveRange(postsToDelete);

                await context.SaveChangesAsync();

                // lub korzystając z BulkDelete

                // await context.BulkDeleteAsync(postsToDelete);



                stopwatch.Stop();
                Console.WriteLine(stopwatch.ElapsedMilliseconds);
            }

        }


        public static async Task Trasanctions01()
        {
            var stopwatch = new Stopwatch();


            using (var context = new ApplicationDbContext())
            {

                var category = new Category() { Id = 15 };
                context.Categories.Add(category);
                context.Categories.Remove(new Category { Id = 5 });
                context.Categories.Remove(new Category { Id = 9999 });
                context.SaveChanges();
            }

        }

        public static async Task Trasanctions02()
        {
            var stopwatch = new Stopwatch();


            using (var context = new ApplicationDbContext())
            {

                using (var transaction = await context.Database.BeginTransactionAsync())
                {
                    var category = new Category() { Id = 15 };
                    await context.Categories.AddAsync(category);
                    context.Categories.Remove(new Category { Id = 5 });
                    context.Categories.Remove(new Category { Id = 9999 });
                    await context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }

            }

        }


        public static async Task Trasanctions03()
        {
            var stopwatch = new Stopwatch();


            using (var context = new ApplicationDbContext())
            {

                using var transaction = await context.Database.BeginTransactionAsync();

                var category = new Category() { Id = 15 };
                await context.Categories.AddAsync(category);
                context.Categories.Remove(new Category { Id = 5 });
                context.Categories.Remove(new Category { Id = 9999 });
                
                await context.SaveChangesAsync();
                await transaction.CommitAsync();
            }

        }


        public static async Task Concurrency01()
        {
            var stopwatch = new Stopwatch();

            using (var context1 = new ApplicationDbContext())
            {
                var category1 = await context1.Categories.FindAsync(2);
                category1.Description = "123aaa";

                using (var context2 = new ApplicationDbContext())
                {
                    var category2 = await context2.Categories.FindAsync(2);
                    
                    category2.Description = "456aaa";
                    DisplayEntitiesInfo(context2);
                    
                    await context1.SaveChangesAsync();
                    await context2.SaveChangesAsync();

                }

            }

        }


        public static async Task Concurrency02()
        {
            var stopwatch = new Stopwatch();

            using (var context1 = new ApplicationDbContext())
            {
                var category1 = await context1.Categories.FindAsync(2);
                category1.Description = "123";

                using (var context2 = new ApplicationDbContext())
                {
                    var category2 = await context2.Categories.FindAsync(2);
                    
                    category2.Description = "456";
                    DisplayEntitiesInfo(context2);

                    try
                    {
                    await context1.SaveChangesAsync();
                    await context2.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException exception)
                    {

                        foreach (var item in exception.Entries)
                        {
                            if (item.Entity is Category)
                            {
                                var proposedValues = item.CurrentValues;
                                var databaseValues = item.GetDatabaseValues();
                                foreach (var property in proposedValues.Properties)
                                {
                                    var proposedValue = proposedValues[property];
                                    var databaseValue = databaseValues[property];
                                    if (proposedValue != databaseValue)
                                    {
                                        // możemy w tym miejscu dać użytkownikowi do wyboru
                                        // która wartość będzie dla niego bardziej odpowiednia

                                    }
                                    
                                }
                            }
                        }
                    }



                }

            }

        }

        public static async Task ViewTest01()
        {
            var stopwatch = new Stopwatch();

            using (var context1 = new ApplicationDbContext())
            {
                var users = await context1.UserFullInfo.ToListAsync();
                foreach (var item in users)
                {
                    Console.WriteLine($"{item.Id} - {item.Login} - {item.Email}");
                }

            }

        }

        public static async Task ProcedureTest01()
        {
            var stopwatch = new Stopwatch();

            using (var context1 = new ApplicationDbContext())
            {
                // wywołujemy na DbSet Post
                var posts = await context1.Posts
                    .FromSqlInterpolated($"AllPostInCategory {3}")
                    .ToListAsync();

                foreach (var item in posts)
                {
                    Console.WriteLine($"{item.Id} - {item.Title}");
                }

            }

        }

        public static async Task ProcedureTest02()
        {
            var stopwatch = new Stopwatch();

            using (var context1 = new ApplicationDbContext())
            {
                // wywołujemy na kontekście
                var posts = await context1.Database
                    .ExecuteSqlInterpolatedAsync($"DeleteArticle {3}");
            
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
