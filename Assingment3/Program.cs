using System.Runtime.Intrinsics.X86;

namespace Assingment3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Q10
            int copies = 100;
            object obj = copies;
            int newCopies = (int)obj;
            #endregion

            #region Q11
            int? year = null;
            Console.WriteLine(year);
            year = 2023;
            Console.WriteLine(year);
            #endregion

            #region Q12
            string? reviewer = null;
            Console.WriteLine(reviewer);
            #endregion

            #region Q13
            Book? book = null;
            Console.WriteLine(book?.Title);
            #endregion

            #region Q14
            Console.WriteLine(book?.Title ?? "Untitled");
            book?.Title ??= "Untitled";
            #endregion

            #region Q15
            string? name = "Ahmed"; //yes
            string confirmedName = name!;
            #endregion

            /////////////////////////////////////////////
            /// Assignments 3
            #region Q1
            Console.WriteLine("Q1");
            int pages = 464;
            bool isAvailable = true;
            if (pages > 300 && isAvailable)
                Console.WriteLine("The book is available and has more than 300 pages.");
            #endregion
            #region Q2
            Console.WriteLine("Q2");
            string title = "Refactoring";
            switch (title) {
                case "Clean Code": Console.WriteLine("Great choice!");
                    break;
                case "Refactoring": Console.WriteLine("Nice pick!");
                    break;
                default: Console.WriteLine("Never heard of it");
                    break;
            }
            #endregion
            #region Q3
            Console.WriteLine("Q3");
            pages = 464;
            string sizeLabel = pages > 300 ? "Long Book" : "Short Book";
            Console.WriteLine(sizeLabel);
            #endregion
            #region Q4
            Console.WriteLine("Q4");
            string[] books = { "Clean Code", "The Pragmatic Programmer", "Refactoring" };
            int i = 0;
            for (i = 0; i < books.Length; i++)
            {
                Console.WriteLine($"Book {i + 1}: {books[i]}");
            }
            #endregion
            #region Q5
            Console.WriteLine("Q5");
            i = 0;
            while (i < 3)
            {
                Console.WriteLine($"Book {i + 1}: {books[i]}");
                i++;
            }
            #endregion
            #region Q6
            Console.WriteLine("Q6");
            i = 0;
            do {
                Console.WriteLine("Checking book...");
                i++;
            } while (i < 3);
            #endregion
            #region Q7
            Console.WriteLine("Q7");
            foreach (string BookStr in books)
            {
                Console.WriteLine(BookStr);
            }
            #endregion
            #region Q8
            Console.WriteLine("Q8");
            for (int j = 0; j < books.Length; j++)
            {
                if (books[j] == "Refactoring")
                    break;
                else
                    Console.WriteLine($"Book {j + 1}: {books[j]}");

            }
            #endregion
            #region Q9
            Console.WriteLine("Q9");
            foreach (string BookStr in books)
            {
                if (BookStr == "The Pragmatic Programmer")
                    continue;
                Console.WriteLine("Book: " + BookStr);
            }
            #endregion
            #region Q10
            Console.WriteLine("Q10");
            new Program().PrintFirstBook(books);
            #endregion

        }
        void PrintFirstBook(Array book)
        {
            if (book.Length == 0) return;
            Console.WriteLine("First book: " + book.GetValue(0));
        }

    }
    class Book
    {
        public string Title { get; set; } = "";
        public string Pages { get; set; } = "0";
    }
}
