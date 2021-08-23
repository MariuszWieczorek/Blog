using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.UI
{
    public static class Repository
    {
        public static List<int> GetNumbers()
        {
            return new List<int>() { 1, 5, 7, 22, 0, 13, 17, 2, 4, 9, 33, 44, 49, 50, 55, 99, 314 };
        }

        public static List<Author> GetAuthors()
        {
            var authors = new List<Author>()
            {
                new Author {Id = 1, Name = "Jan Kowalski" },
                new Author {Id = 2, Name = "Marek Nowak" }
            };

            return authors;
        }


        public static List<Book> GetBooksIHaveRead()
        {
            var books = new List<Book>()
            {
                new Book()
                {
                    Id = 1,
                    Title = "Jak zostać Programistą",
                    Price = 23.20M,
                    AuthorId = 1

                },
                new Book()
                {
                    Id = 2,
                    Title = "Podstawy C#",
                    Price = 49M,
                    AuthorId = 2

                },

                new Book()
                {
                    Id = 22,
                    Title = "Czysty Kod",
                    Price = 1M,
                    AuthorId = 2

                },
            };
            return books;
        }

        public static List<Book> GetBooks()
        {

            
            
            var books = new List<Book>()
            {
                new Book()
                {
                    Id = 1,
                    Title = "Jak zostać Programistą",
                    Price = 23.20M,
                    AuthorId = 1

                },
                new Book()
                {
                    Id = 2,
                    Title = "Podstawy C#",
                    Price = 49M,
                    AuthorId = 2

                },
                new Book()
                {
                    Id = 3,
                    Title = "Entity Framework Core",
                    Price = 114M,
                    AuthorId = 1

                },
                new Book()
                {
                    Id = 4,
                    Title = "Platforma .Net",
                    Price = 50M,
                    AuthorId = 1

                },
                new Book()
                {
                    Id = 5,
                    Title = "Pigułka wiedzy C#",
                    Price = 120.12M,
                    AuthorId = 2

                },
                new Book()
                {
                    Id = 6,
                    Title = "Testy Jednostkowe",
                    Price = 32M,
                    AuthorId = 2

                },

                 new Book()
                {
                    Id = 7,
                    Title = "PHP Tablice",
                    Price = 2M,
                    AuthorId = 1

                },

                  new Book()
                {
                    Id = 8,
                    Title = "C# Tablice",
                    Price = 2M,
                    AuthorId = 1

                },
            };

            return books;
        }
    }
}
