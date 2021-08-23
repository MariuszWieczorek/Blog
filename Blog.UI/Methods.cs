using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.UI
{
    public static class Methods
    {

        public static void Linq01()
        {
            var numbers = Repository.GetNumbers();

            Console.WriteLine("LINQ Query Syntax");
            var evenNumbersQuerySyntax = from x in numbers
                                         where x % 2 == 0
                                         select x;

            foreach (var item in evenNumbersQuerySyntax)
            {
                Console.WriteLine(item);
            }


            Console.WriteLine("LINQ Method Syntax - Lambda Expresions");
            // oczekujemy delegata predicate funk, możemy przekazać wyrażenie lambda lub funkcję
            var evenNumbersMethodSyntax1 = numbers.Where(x => x % 2 == 0);

            foreach (var item in evenNumbersMethodSyntax1)
            {
                Console.WriteLine(item);
            }

            var evenNumbersMethodSyntax2 = numbers.Where(IsEventNumber);
        }


        public static void Linq02_Where_Any_All()
        {

            var books = Repository.GetBooks();
            var booksCheaperThan50 = books.Where(x => x.Price < 50);
            var csharpBooksCheaperThan50 = books.Where(x => x.Price < 50 && x.Title.Contains("C#"));


            foreach (var item in csharpBooksCheaperThan50)
            {
                Console.WriteLine($"{ item.Title} {item.Price}" );
            }


            bool isAnyBookCheaperThan50 = books.Any(x => x.Price < 10);
            Console.WriteLine(isAnyBookCheaperThan50);

            bool isAllBookIdGreaterThan0 = books.All(x => x.Id > 0);
            Console.WriteLine(isAllBookIdGreaterThan0);

        }


        public static void Linq03_OrderBy()
        {
            var books = Repository.GetBooks();
            var csharpBooksOrderedByPrice = books
                .Where(x => x.Title.Contains("C#"))
                .OrderBy(x => x.Price);

            var csharpBooksOrderedDescendingByPrice = books
                .Where(x => x.Title.Contains("C#"))
                .OrderByDescending(x => x.Price);

            var csharpBooksOrderedByPriceAndId = books
                .OrderBy(x => x.Price)
                .ThenBy(x => x.Id);

            PrintBooks(csharpBooksOrderedByPriceAndId);


        }

        public static void Linq04_SingleElement()
        {
            // Aby wybrać pojedynczy element mamy do wybory 4 metody
            // First()  FirstOrDefault() Single() SingleOrDefault()
            // Oprócz tego są jeszcze metody Last() i LastOrDefault()

            var books = Repository.GetBooks();
            
            // Gdy istnieje conajmniej jeden to pierwszy znaleziony zostaje pobrany
            // i zostaje zakończone przeszukiwanie listy
            // Gdy nie ma żadnego, zostaje rzucony wyjątek
            var bookWithPrice2 = books.First(x => x.Price == 2);



            PrintBook(bookWithPrice2);

            // Gdy istnieje conajmniej jeden to pierwszy znaleziony zostaje pobrany
            // i zostaje zakończone przeszukiwanie listy
            // Gdy nie ma żadnego, zostaje zwrócony null
            var bookWithPrice3 = books.FirstOrDefault(x => x.Price == 3);
            PrintBook(bookWithPrice3);

            // Single()
            // Obiekt zostanie zwrócony gdy dokładnie jeden element spełnia warunek
            // Gdy istnieje więcej niż jeden element spełniający warunek
            // w pozostałych przypadkach zostanie rzucony wyjątek

            // SingleOrDefault()
            // Obiekt zostanie zwrócony gdy dokładnie jeden element spełnia warunek
            // Gdy żaden element nie spełnia warunków
            // zostaje zwrócona wartość domyślna
            // Gdy istnieje więcej niż jeden element spełniający warunek
            // zostanie rzucony wyjątek
            var bookWithPrice3b = books.SingleOrDefault(x => x.Price == 3);
            PrintBook(bookWithPrice3b);

            
            
            // Single() lub SingleOrDefault() stosujemy
            // jeżeli wiesz, że powinien być tylko jeden rekord

            // Wyrażenia First() i FirstOrDefault() są szybsze niż Single() i SingleOrDefault()

            // możliwa jest skłądnia jak poniżej, ale jest wolniejsza i nie zalecana
            // dużo lepiej jest przekazać predykat bezpośrednio do FirstOrDefault
            var bookWithPrice3c = books.Where(x => x.Price == 3).FirstOrDefault();

        }

        public static void Linq05_IEnumerable()
        {
            // Odroczone wykonanie na interfejsie IEnumerable
            // Deferred Execution of LINQ Query
            // czyli zostanie ono wykonane dopiero wtedy, gdy
            // próbujesz się odnieść do tej kolekcji
            
            var books = Repository.GetBooks();
            IEnumerable<Book> booksWithIdGreaterThan5 = books
                .Where(x => x.Id > 5);

            // Jeżeli pracowalibyśmy na liście .ToList() to były by inne wyniki
            // dodane później rekordy nie były by uwzględnione

            books.Add(new Book() { Id = 9, Title = "Testy jednostkowe A", Price = 50 });
            books.Add(new Book() { Id = 10, Title = "Testy jednostkowe B", Price = 50 });


            PrintBooks(booksWithIdGreaterThan5);

            // mimo iż teoretycznie książki o Id 9  i 10 nie istniały w moencie przypisania
            // zostają one wyświetlone

            books.Add(new Book() { Id = 11, Title = ".Net Core", Price = 50 });
            books.Add(new Book() { Id = 12, Title = "LINQ", Price = 50 });

            PrintBooks(booksWithIdGreaterThan5);

        }


        public static void Linq06_Select()
        {
            // Projektowanie wyniku zapytania
            // Jeżeli nie potrzebujemy wszystkich danych należy pamiętać o stosowaniu selecta
            

            var books = Repository.GetBooks();
            var bookTitlesWithIdGreaterThan5 = books
                .Where(x => x.Id > 5)
                .Select(x => x.Title);

            foreach (var item in bookTitlesWithIdGreaterThan5)
            {
                Console.WriteLine($"{item}");
            }

            // możemy zrzucić kolekcję zupełnie innych obiektów np. klasa MyBook

            var myBooks = books
                .Where(x => x.Id > 5)
                .Select(x =>
                new MyBook { Info = $"{x.Id} - {x.Title}" }
                );

            Console.WriteLine("-----------------------");

            foreach (var item in myBooks)
            {
                Console.WriteLine($"{item.Info}");
            }


            // możemy zrzucić kolekcję zupełnie innych obiektów np. klasa anonimowej
            var myBooks2 = books
                .Where(x => x.Id > 5)
                .Select(x =>
                new { InfoExtended = $"{x.Id} - {x.Title} cena {x.Price} PLN" }
                );

            Console.WriteLine("-----------------------");

            foreach (var item in myBooks2)
            {
                Console.WriteLine($"{item.InfoExtended}");
            }

        }

        public static void Linq07_Distinct()
        {
            // służy do wyeliminowania powtórzeń
            var numbers = new List<int>() { 1, 2, 3, 4, 4, 5, 6, 6, 6 };

            // w przypadku prostych typów takich jak int, to zadziała
            var uniqueNumbers = numbers.Distinct();

            foreach (var item in uniqueNumbers)
            {
                Console.WriteLine(item);
            }


            // w przypadku obiektów
            // domyślenie elementy są porównywane po referencji

            var books = Repository.GetBooks();

            books.Add(new Book { Id = 15, Title = "Tytuł A", Price = 25M });
            books.Add(new Book { Id = 15, Title = "Tytuł A", Price = 25M });
            books.Add(new Book { Id = 15, Title = "Tytuł C", Price = 25M });


            // więc kod poniżej nie zadziała poprawnie
            // będą nadal wyświetlone wszystkie obiekty
            var books1 = books.Distinct();
            PrintBooks(books1);


            // musimy wskazać jak klasy mają być porównywane pomiędzy sobą
            // możemy zaimplementować klasę, która powie jak porównywać
            var books2 = books.Distinct(new BookComparer());
            PrintBooks(books2);



        }

        public static void Linq08_Join()
        {
            // złączenia kolekcji

            var books = Repository.GetBooks();
            var authors = Repository.GetAuthors();


            // interesuje nas najpierw books, to jest nasz lista bazowa
            // od niej zaczynamy
            // 1: potem kolekcja, którą chcemy połączyć
            // 2: klucz obcy z kolekcji od której startujemy
            // 3: klucz główny kolekcji, którą dołanczamy
            // 4: następnie projekcja wyników, co ma zostać zwrócone
            // chcemy aby został zwrócony nowy obiekt, może być anonimowy
            // na koniec możemy posortować
            var booksWithAuthors = books.Join(
                  authors
                , b => b.AuthorId
                , a => a.Id
                , (b, a) => new { Title = b.Title, AuthorName = a.Name })
                .OrderBy(x => x.AuthorName);
                

            foreach (var item in booksWithAuthors)
            {
                Console.WriteLine($"{item.Title} {item.AuthorName}");
            }

        }


        public static void Linq09_Except_Intersect_Union()
        {

            var books = Repository.GetBooks();
            var booksIHaveRead = Repository.GetBooksIHaveRead();

            // chcę uzyskać książki, których jeszcze nie czytałem
            // w Except wskazuję
            // drugą kolekcję
            // oraz sposób w jaki te książki mają zostać porównane

            Console.WriteLine("\n --- Except --- Książki, których nie przeczytałem");
            var booksIHaveNotRead = books
                .Except(booksIHaveRead, new BookComparer());

            PrintBooks(booksIHaveNotRead);

            Console.WriteLine("\n --- Intersect Książki, które są w kolekcji books, które przeczytałem ---");
            var commonBooks = books
                .Intersect(booksIHaveRead, new BookComparer());

            PrintBooks(commonBooks);


            Console.WriteLine("\n --- Union Książki, z obydwu kolekcji, bez powtórzeń ---");
            var allDistinctBooks = books
                .Union(booksIHaveRead, new BookComparer());

            PrintBooks(allDistinctBooks);

        }


        public static void Linq10_AggregateFunctions()
        {
            var books = Repository.GetBooks();

            // var book = books.Max(); Wyjątek, bo aby to zadziałało musi być zaimplementowany interfejs IComperable
            // bo Linq nie wie po jakich wartościach ma porównywać

            var theBiggestPrice1 = books.Select(x => x.Price).Max();

            // lub lepsze rozwiązanie, przekazanie selektora jako parametr

            var theBiggestPrice2 = books.Max(x => x.Price);
            var theLowestPrice = books.Min(x => x.Price);
            var theAveragePrice = books.Average(x => x.Price);
            var totalPrice = books.Sum(x => x.Price);
            var numberOfBooks = books.Count();
            var numberOfBooksWithPriceGreaterThan50 = books.Count(x=>x.Price>50);

        }


        public static void Linq11_DataGrouping()
        {
            // możemy pogrupować po autorach i wyświetlić
            // wszystkich autorów wraz z ich książkami
            
            var books = Repository.GetBooks();
            var authors = Repository.GetAuthors();


            // mamy książki z ich autorami
            var authorsWithBooks = books.Join(
                  authors
                , b => b.AuthorId
                , a => a.Id
                , (b, a) => new { Book = b, Author = a });

            // mamy książki z ich autorami
            // grupujemy po autorze
            // następnie wyświetlamy autora i po przecinku jego książki

            var authorsWithBooksGrouping = books.Join(
                  authors
                , b => b.AuthorId
                , a => a.Id
                , (b, a) => new { Book = b, Author = a })
                .GroupBy(x => x.Book.AuthorId)
                .Select(x => new
                {
                    AuthorId = x.Key,
                    Titles = string.Join(", ", x.Select( x => x.Book.Title)),
                    AuthorName = x.First().Author.Name
                });


            foreach (var item in authorsWithBooksGrouping)
            {
                Console.WriteLine($"author {item.AuthorName} : books {item.Titles} ");
            }

        }

            private static bool IsEventNumber(int number)
        {
            return number % 2 == 0;
        }





        private static void PrintBooks(IEnumerable<Book> books)
        {
            Console.WriteLine("-----------------------------------------------");
            foreach (var item in books)
            {
                Console.WriteLine($"{item.Id} {item.Title} {item.Price,1:C} PLN");
            }
        }


        private static void PrintBook(Book item)
        {

                Console.WriteLine($"{item?.Id} {item?.Title} {item?.Price,1:C} PLN");

        }

    }

   

    
}
