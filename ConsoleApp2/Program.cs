using PlayGround;
using System.Numerics;
using System.Text;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Q1
            Book client = new();
            //Console.WriteLine(Book.password); // This line will cause a compilation error because 'password' is a private member of the Client class and cannot be accessed from outside the class.
            #endregion

            #region Q2
            client.copiesInStock = 10; // yes, cause copiesInStock is an internal member of the Book class and can be accessed from within the same assembly.
            #endregion
            #region Q3
            client.Title = "New Book";
            Console.WriteLine(client.Title);
            #endregion
            #region Q4
            Console.WriteLine(client.Genre);
            #endregion
            #region Q5
            foreach (Genre genre in Enum.GetValues<Genre>())
            {
                Console.WriteLine((int)genre);
            }
            #endregion
            #region Q6
            int genreNumber = 1;
            Console.WriteLine((Genre)genreNumber);
            #endregion

            #region Q7
            Genre genre2 = Genre.Fiction;
            Console.WriteLine(genre2.ToString());
            #endregion
            #region Q8
            string genreText = "Science";
            Genre g3 = Enum.Parse<Genre>(genreText);
            Console.WriteLine(g3);
            #endregion

            #region Q9
            genreText = "Mystery";
            if(Enum.TryParse<Genre>(genreText, out Genre g))
                Console.WriteLine(g);
            else
                Console.WriteLine("Unknown genre");
            #endregion

        }
    }    

    class Book
    {
      internal  int copiesInStock = 5;
        private string password = "secret";
        public string? Title;
       public Genre Genre = Genre.Science;
    }
    enum Genre { Fiction, NonFiction, Science }
}