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

                // methods.Linq02_Where_Any_All();
                // Methods.Linq03_OrderBy();
                // Methods.Linq04_SingleElement();
                // Methods.Linq05_IEnumerable();
                // Methods.Linq06_Select();
                // Methods.Linq07_Distinct();
                // Methods.Linq08_Join();
                // Methods.Linq09_Except_Intersect_Union();
                // Methods.Linq10_AggregateFunctions();
                // Methods.Linq11_DataGrouping();
                Methods.Linq12_Pagination_Take_Skip();

                Console.ReadLine();
            }
        }

        
    }
}
