using Blog.DataLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Blog.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new ApplicationDbContext() )
            {
                context.Database.Migrate();
                var post = context.Posts.FirstOrDefault();
                Console.WriteLine(post.Description);
            }
        }
    }
}
