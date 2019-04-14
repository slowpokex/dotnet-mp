using System;
using SpringNet;

namespace SpringNetDemo
{
    public class SpringNetDemo
    {
        public static void Main(string[] args)
        {
            var context = new ApplicationContext();
            context.Initialize();
            Console.ReadKey();
        }
    }
}
