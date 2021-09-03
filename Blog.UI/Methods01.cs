using Blog.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.UI
{
    public static class Methods01



    {

        public static void task01()
        {
            using (var context = new ApplicationDbContext())
            {
                //1) Pobierz wszystkich studentów, którzy mają więcej niż 25 lat.
                var studentsOlderThan25 = context.Students
                    .Where(x=>x.Age>25);
                Console.WriteLine("---- Students older than 25 ---- ");
                foreach (var item in studentsOlderThan25)
                {
                    Console.WriteLine($"{item.Name} {item.Age}");
                }
            }
        }

        public static void task02()
        {
            using (var context = new ApplicationDbContext())
            {
                //2) Sprawdź czy istnieje jakiś student, który ma więcej niż 40 lat.
                bool isExistsStudentOlderThan40 = context.Students
                    .Any(x => x.Age > 40);
                Console.WriteLine("---- are there students older than 40 ---- ");
                Console.WriteLine(isExistsStudentOlderThan40);
            }
        }

        public static void task03()
        {
            using (var context = new ApplicationDbContext())
            {
                //3) Pobierz wszystkich studentów o imieniu Marcin.
                var studentsWithNameMarcin = context.Students
                    .Where(x => x.Name.Contains("Marcin"));
                
                Console.WriteLine("---- Students with name Marcin ---- ");
                foreach (var item in studentsWithNameMarcin)
                {
                    Console.WriteLine($"{item.Id} {item.Name} {item.Age}");
                }
            }
        }

        public static void task04()
        {
            using (var context = new ApplicationDbContext())
            {
                //4) Pobierz tylko nazwę każdego studenta
                //   posortuj ich najpierw po nazwie,
                //   następnie malejąco po Id
                var studentsNameSortedByNameAndByIdDesc = context.Students
                   .OrderBy(x => x.Name)
                   .ThenByDescending(x => x.Id)
                   .Select(x => x.Name);

                Console.WriteLine("---- only Students names sorted by name and id ---- ");
                foreach (var item in studentsNameSortedByNameAndByIdDesc)
                {
                    Console.WriteLine($"{item}");
                }
            }
        }

        public static void task04a()
        {
            using (var context = new ApplicationDbContext())
            {
                //4) Pobierz tylko nazwę każdego studenta
                //   posortuj ich najpierw po nazwie,
                //   następnie malejąco po Id
                var studentsNameSortedByNameAndByIdDesc = context.Students
                   .OrderBy(x => x.Name)
                   .ThenByDescending(x => x.Id)
                   .Select(x => new { StudentName = x.Name });

                Console.WriteLine("---- only Students names sorted by name and id ---- ");
                foreach (var item in studentsNameSortedByNameAndByIdDesc)
                {
                    Console.WriteLine($"{item.StudentName}");
                }
            }
        }


        public static void task05()
        {
            using (var context = new ApplicationDbContext())
            {
                //5) Pobierz do jednej kolekcji informację
                //   o nazwie studenta i opisie jego grupy.
                var studentsNameWithGroupName = context.Students
                    .Join(context.Groups
                    , s => s.GroupId
                    , g => g.Id
                    , (s, g) => new { StudentName = s.Name, GroupName = g.Name });


                Console.WriteLine("---- Students With Their Group ----");
                foreach (var item in studentsNameWithGroupName)
                {
                    Console.WriteLine($"{item.StudentName} {item.GroupName}");
                }
            }
        }

        public static void task06()
        {
            using (var context = new ApplicationDbContext())
            {
                //6) Wyświetl średnią wieku wszystkich studentów.
                var avarageAgeOfStudents = context.Students
                    .Average(x => x.Age);

                Console.WriteLine("---- average age of all students  ---- ");
                Console.WriteLine(avarageAgeOfStudents);
            }
        }

        public static void task07()
        {
            using (var context = new ApplicationDbContext())
            {
                //7) Pogrupuj studentów po grupie
                //   wyświetl wszystkich studentów należącej do danej grupy po przecinku.
                //   gdy nie przekonwertuję na listy, będzie wyjątek
                //   
                var students = context.Students.ToList();
                var groups = context.Groups.ToList();

                var groupsWithStudents = students.Join(
                      groups
                    , s => s.GroupId
                    , g => g.Id
                    , (s, g) => new { Student = s, Group = g})
                    .GroupBy(x => x.Student.GroupId)
                    .Select(x => new
                    { 
                        GroupId = x.Key,
                        Students = string.Join(", ", x.Select(x => x.Student.Name)),
            

                    }
                    ).ToList();


                Console.WriteLine("---- Groups with students ----");
                foreach (var item in groupsWithStudents)
                {
                    Console.WriteLine($"{item.GroupId} {item.Students}");
                }
            }
        }
        public static void task08()
        {


            using (var context = new ApplicationDbContext())
            {


                //8) Zastosuj paginacje
                //   wyświetl 2 stronę z listą studentów
                //   zawierającą 10 rekordów.

                int pageNumber = 2;
                int pageSize = 2;

                var students = context.Students
                    .Skip((pageNumber - 1)*pageSize)
                    .Take(pageSize);


                Console.WriteLine($"page: {pageNumber} records {pageSize}");
                foreach (var item in students)
                {
                    Console.WriteLine($"{item.Name}");
                }
                



            }
        }
    }
}
