namespace GreetingApp
{
    using System;
    using GreetingClassLibrary;

    public class Greetings
    {
        static void Main(string[] args)
        {
            SharedGreetings.GetGreeting();
            if (args.Length == 0)
            {
                Console.WriteLine(SharedGreetings.GetGreeting());
            }
            else if (args.Length == 1)
            {
                Console.WriteLine(SharedGreetings.GetGreeting(args[0]));
            }
            else
            {
                Console.WriteLine(SharedGreetings.GetGreeting(args[0], args[1]));
            }
            Console.ReadKey();
        }
    }
}
