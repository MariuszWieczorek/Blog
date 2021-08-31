using Blog.DataLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blog.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new ApplicationDbContext() )
            {

                // pobieramy wszystki posty
                var posts = context.Posts;
                var userWithId3 = context.Users.Find(3);

                Console.WriteLine(userWithId3.Login);
                foreach (var item in posts)
                {
                    Console.WriteLine(item.Title);
                }
                 


                Console.ReadLine();
            }
        }

        
    }
}
