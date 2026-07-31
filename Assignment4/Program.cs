using System.Text;

namespace Assignment4;

    internal class Program
    {
        static void Main(string[] args)
        {
            #region Q1
            Console.WriteLine("Q1");
            string title = "clean code";
            string upperTitle = title.ToUpper();
            Console.WriteLine($"Title: {title}, Upper: {upperTitle}");
            #endregion
            #region Q2
            Console.WriteLine("Q2");
            string s1 = "Clean Code";
            string s2 = "Clean Code";
            Console.WriteLine($"{object.ReferenceEquals(s1, s2)}");
            #endregion
            #region Q3
            Console.WriteLine("Q3");
            StringBuilder sb = new();
            sb.Append("Book List");
            sb.Append(" - Updated");
            Console.WriteLine(sb);
            #endregion
            #region Q4
            Console.WriteLine("Q4");
            sb.Replace("Book List", "Library");
            Console.WriteLine(sb);
            #endregion Q5
            Console.WriteLine("Q5");
            title = "Clean Code";
            int pages = 464;
            string newTitle = "Book: " + title +" Pages: "+ pages;
            Console.WriteLine(newTitle);
            #region Q6
            Console.WriteLine("Q6");
            newTitle = $"Book: {title} Pages: {pages}";
            Console.WriteLine(newTitle);
            #endregion
            #region  Q7
            newTitle = string.Format("Book: {0}, Pages: {1:0}", title, pages);
            Console.WriteLine(newTitle);
            #endregion
            #region Q8
            int? pages2 = null;
            Console.WriteLine(pages2 ?? 0);
            #endregion
            #region Q9
            string? author = null;
            Console.WriteLine(author?.Length);
            #endregion

            #region Q10
            Console.WriteLine("Q10");
            new Program().PrintWelcomeMessage();
        #endregion

        #region Q11
        Console.WriteLine("Q11");
        new Program().PrintBookTitle("Clean Code");
        #endregion
    }

        void PrintWelcomeMessage()
        {
            Console.WriteLine("Welcome to the Library!");
        }

    void PrintBookTitle(string title)
    {
        Console.WriteLine("Book title: " + title);
    }
}
