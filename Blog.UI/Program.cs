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

               

                Console.ReadLine();
            }
        }

        
    }
}
