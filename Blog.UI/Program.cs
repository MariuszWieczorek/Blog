using System;
using System.Threading.Tasks;

namespace Blog.UI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Methods.ExplicitLoading01();
            // Methods.ExplicitLoading02();
            // Methods.ExplicitLoading03();

            // Methods.IQueryableVsIEnumerable04();
            // Methods.ChangeTracker01();
            // Methods.Inne01();
            // Methods.RawSQL01();


            // Zadanie domowe LINQ
            // Methods01.task01();
            // Methods01.task02();
            // Methods01.task03();
            // Methods01.task04();
            // Methods01.task04a();
            // Methods01.task05();
            // Methods01.task06();
            // Methods01.task07();
            // Methods01.task08();

            // Komendy
            // await Methods02.Task01();
            // await Methods02.Task02();
            // await Methods02.Task03();
            // await Methods02.Task04();
            // await DeleteMethods.Concurrency01();
            await DeleteMethods.ProcedureTest02();


            Console.ReadLine();
        }

        
    }
}
