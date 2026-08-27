namespace Advanced;

class Program
{
    static void Main(string[] args)
    {
        /*
         * Q1)
         * A generic class is a blueprint for a class that can work with any data type,
         * which you specify when you create an object.
         *Q3
         * Multiple type parameters allow
         * a generic class or method to work with more than one placeholder data type at the same time
         *
         * Q4
         * A generic method is a method declared with its own type parameters
         *
         *
         *Q6
         *  an interface declared with type parameters,
         * serving as a reusable contract that defines methods
         * or properties without tying them to a specific data type
         *
         *Q7
         * The struct constraint specifies that the type parameter must be a value type
         *
         * Q8
         * The class constraint specifies that the type parameter must be a reference type
         *
         * Q9
         * The new() constraint specifies that the type parameter must have a public, parameterless constructor
         *
         *Q10
         * The interface constraint specifies that the type parameter must implement a specific interface
         *
         * Q11
         *The base class constraint specifies that the type parameter must inherit from a specific base class
         * Q12
         *To apply multiple constraints to a single type parameter, separate them with a comma after the where keyword
         *
         *
         *
         */
    ValueInspector<int> intInspector = new ValueInspector<int>(); 
    ReferenceChecker<Customer> customerChecker = new ReferenceChecker<Customer>();
        
    Factory<Car> carFactory = new Factory<Car>();
    Car myCar = carFactory.CreateInstance();
    
    AnimalShelter<Dog> dogShelter = new AnimalShelter<Dog>();
    
    }
    
    public class Manager { }
    public interface IDisposable { void Dispose(); }

    public class WorkerFactory<T> where T : Manager, IDisposable, new()
    {
        public T CreateAndCleanUp()
        {
            T worker = new T(); 
            worker.Dispose(); 
            return worker;
        }
    }

    
    public class Animal { public string Name { get; set; } }
    public class Dog : Animal { }

    public class AnimalShelter<T> where T : Animal
    {
        private List<T> _animals = new();

        public void PrintNames()
        {
            foreach (var animal in _animals)
            {
                Console.WriteLine(animal.Name); 
            }
        }
    }

    public interface ILoggable
    {
        void Log();
    }

    
    
    public class LoggerUtility
    {
        public static void RunAndLog<T>(T item) where T : ILoggable
        {
            item.Log(); 
        }
    }

    
    public class Factory<T> where T : class, new()
    {
        public T CreateInstance()
        {
            return new T(); 
        }
    }

    public class Car 
    { 
        public Car() { /* Parameterless constructor */ } 
    }


    public class ReferenceChecker<T> where T : class
    {
        // Safe to check for null because T is guaranteed to be a reference type
        public bool IsNull(T item)
        {
            return item == null;
        }
    }


    public class ValueInspector<T> where T : struct
    {
        // Since T is a value type, we can safely reset it to its default non-null state
        public void PrintDefaultValue()
        {
            T defaultValue = default(T);
            Console.WriteLine($"Default value: {defaultValue}");
        }
    }



    public interface IRepository<T>
    {
        void Add(T entity);
        T GetById(int id);
        IEnumerable<T> GetAll();
        void Update(T entity);
        void Delete(int id);
    }

    
    public class Utility
    {
        public static T FindMax<T>(T a, T b) where T : IComparable<T>
        {
            return a.CompareTo(b) > 0 ? a : b;
        }
        
        public static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }
    }
    public class Pair<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }

        public Pair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }
    
    public class Container<T>
    {
        private T? _item;

        public void Add(T item)
        {
            _item = item;
        }

        public T Get()
        {
            return _item;
        }
    }
}

internal class Customer
{
}