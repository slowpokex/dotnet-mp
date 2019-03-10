using System;
using FileSystemVisitorLib;
using FileSystemVisitorLib.FileEventObservers;

namespace FileSystemVisitorRunner
{
    internal class Runner
    {
        private static void Main(string[] args)
        {
            var fileSystemVisitor = new FileSystemVisitor("D:/Projects", item => item.Contains("dotnet-mp"));

            var fileEventObserver = new EventObserver();
            fileEventObserver.Subscribe(fileSystemVisitor.GetFileSystemEventObservable());

            foreach (var fileItems in fileSystemVisitor.GetFileItems()){
                if (fileEventObserver.ShouldSkip(fileItems))
                {
                    continue;
                }
                if (fileEventObserver.ShouldInterrupt(fileItems))
                {
                    break;
                }
                Console.WriteLine(fileItems);
            }

            fileEventObserver.Unsubscribe();
            Console.ReadKey();

        }
    }
}
