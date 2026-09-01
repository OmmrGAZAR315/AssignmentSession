namespace Advanced;

class Program
{
    static void Main(string[] args)
    {
        List<int> grades = [85, 92, 78, 95, 88, 70, 100, 65];
        Console.WriteLine(string.Join(",", grades));
        Console.WriteLine(grades.Count());
        grades.Sort();
        Console.WriteLine(grades[1]);
        Console.WriteLine(grades[^1]);
        Console.WriteLine(grades.FindAll(x => x > 90));
        Console.WriteLine(grades.RemoveAll(x => x < 75));
        Console.WriteLine(grades.Contains(100));
        var gradesStr = grades.ConvertAll(x => $"Grade: {x}");
        Console.WriteLine(string.Join(",", gradesStr));

        var leaderboard = new SortedList<int, string>()
        {
            { 500, "Ahmed" },
            { 200, "Sara" },
            { 800, "Ali" },
            { 350, "Mona" },
        };
        Console.WriteLine(string.Join(",", leaderboard));
        Console.WriteLine(leaderboard.First());
        Console.WriteLine(leaderboard.Last());
        Console.WriteLine(leaderboard.ContainsKey(500));
        if (leaderboard.TryGetValue(999, out var result))
            Console.WriteLine($"Found Player: {result}");
        else
            Console.WriteLine("Not Found");
        leaderboard.Remove(200);
        Console.WriteLine(string.Join(",", leaderboard));

        var phoneContact = new Dictionary<string, string>()
        {
            { "Omar Ahmed", "0122299400" },
            { "Omar Ahmed2", "0122299402" },
            { "Omar Ahmed3", "0122299403" },
            { "Omar Ahmed4", "0122299404" },
        };

        phoneContact["USER"] = "012220023323";
        try
        {
            phoneContact.Add("Omar Ahmed", "012220023323");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        if (phoneContact.TryAdd("Omar Ahmed", "012220023323"))
            Console.WriteLine($"Found Phone: {phoneContact["Omar Ahmed"]}");
        else
            Console.WriteLine("Not Found");

        if (!phoneContact.TryGetValue("Omar Ahmed", out var phone))
            Console.WriteLine("Not Found");
        phoneContact.GetValueOrDefault("Ahmed", "Not Found");
        Console.WriteLine(String.Join(",", phoneContact.Keys));
        Console.WriteLine(String.Join(",", phoneContact.Values));


        var set = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        string[] inputEmails = { "ahmed@test.com", "AHMED@test.com", "sara@test.com", "Sara@Test.Com" };

        Console.WriteLine("--- Adding Emails ---");
        foreach (var email in inputEmails)
        {
            var added = set.Add(email);
            Console.WriteLine($"Adding '{email}'... {(added ? "Success (New)" : "Skipped (Duplicate)")}");
        }

        Console.WriteLine(set.Count());

        HashSet<int> a = [1, 2, 3, 4, 5];
        HashSet<int> temp = new(a);
        HashSet<int> b = [4, 5, 6, 7, 8];

        a.UnionWith(b);
        Console.WriteLine("Union: " + string.Join(", ", a));
        Console.WriteLine(string.Join(", ", temp));

        a = new(temp);
        a.IntersectWith(b);
        Console.WriteLine("Intersection: " + string.Join(", ", a));

        a = temp;
        var isSubset = new HashSet<int> { 1, 2 }.IsSubsetOf(a);
        Console.WriteLine("Is [1, 2] a subset of a? " + isSubset);
    }
}