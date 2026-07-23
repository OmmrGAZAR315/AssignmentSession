namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Q1
            Object obj = new Book { Title = "The Great Gatsby", Pages = "180" };
            Console.WriteLine(obj);
            #endregion;
            #region Q2
            Console.WriteLine(obj.ToString());
            Console.WriteLine(obj.Equals(obj));
            Console.WriteLine(obj.GetHashCode());
            Console.WriteLine(obj.GetType());
            #endregion

            #region Q3
            int pages = Convert.ToInt32("464"); // it was a compile-time error
            // or
            pages = 464;
            Console.WriteLine(pages);
            #endregion

            #region Q4
            try
            {
                int a = 10;
                int b = 0;
                Console.WriteLine(a / b);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot divide by zero");
            }
            #endregion

            #region Q5
            int pagesInt = 300;
            double pagesDouble = pagesInt;
            #endregion

            #region Q6
            double price = 49.99;
            int priceInt = (int)price;
            #endregion

            #region Q7
            string pagesText = "464";
            int pagesNumber = Convert.ToInt32(pagesText);
            #endregion


            #region Q8
            string yearText = "2023";
            int yearNumber = int.Parse(yearText);
            string badText = "abc";
            try{ int badNumber = int.Parse(badText); } catch { Console.WriteLine("Invalid number."); }
            #endregion

            #region Q9
            int pages2 = 464;
            string pagesString = pages2.ToString();
            Console.WriteLine(pagesString.GetType());
            #endregion


        }
    }

    class Book
    {
        public string Title { get; set; }
        public string Pages { get; set; }
    }
}
