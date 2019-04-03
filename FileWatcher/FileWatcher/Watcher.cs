using System;
using System.IO;
using FileWatcher.Configuration;
using Microsoft.Extensions;
using Microsoft.Extensions.Configuration;

namespace FileWatcher
{
    public class Watcher
    {
        static void Main(string[] args)
        {
            Run();
        }

        private static void Run()
        {
            var Configuration = AppConfigurationProvider.getInstance().Configuration;

            Console.WriteLine(Configuration["DefaultPath"]);
            Console.ReadKey();
        }
    }
}
