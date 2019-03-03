using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreetingClassLibrary
{
    public class SharedGreetings
    {
        public static string GetGreeting(string name = "", string age = "all") => $"Hello, {name}! You are {age} years old!";
    }
}
