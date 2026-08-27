using PlayGround;
using System.Numerics;
using System.Text;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main2(string[] args)
        {
                Dog myDog = new Dog();

                // Accessing inherited property and method from Animal
                myDog.Name = "Buddy";
                myDog.Eat();   // Output: Buddy is eating.

                // Accessing Dog's own method
                myDog.Bark();  // Output: Buddy says: Woof!
    }
        class Animal
        {
            public string Name { get; set; }

            public void Eat()
            {
                Console.WriteLine($"{Name} is eating.");
            }
        }

        // Derived Class (Child)
        class Dog : Animal
        {
            public void Bark()
            {
                Console.WriteLine($"{Name} says: Woof!");
            }
        }
    }    

}