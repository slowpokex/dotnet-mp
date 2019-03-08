using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileSystemVisitorLib;

namespace FileSystemVisitorRunner
{
    internal class Runner
    {
        private static void Main(string[] args)
        {
            var fileSystemVisitor = new FileSystemVisitor("D:/Mentoring/dotnet-mp");

            foreach (var fileItems in fileSystemVisitor.GetFileItems())
            {
                Console.WriteLine(fileItems);
            }

            Console.ReadKey();
        }
    }
}
