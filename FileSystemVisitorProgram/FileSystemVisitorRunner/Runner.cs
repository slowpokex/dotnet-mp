using System;
using System.IO;
using FileSystemVisitorLib;
using FileSystemVisitorLib.FileEventObservers;

namespace FileSystemVisitorRunner
{
    internal class Runner
    {
        private static readonly string EntryPath = "D:/Mentoring";

        private static void Main(string[] args)
        {
            var fileEventObserver = new EventObserver();
            var fileEventObservable = new EventObservable();

            try
            {
                var fileSystemVisitor = new FileSystemVisitor(EntryPath, fileEventObservable, item => item.Contains("dotnet-mp"));

                fileEventObserver.Subscribe(fileSystemVisitor.FileSystemEventObservable);

                foreach (var fileItems in fileSystemVisitor.GetItems())
                {
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
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"The specified directory {e.Message} not found");
            }
            finally
            {
                fileEventObserver.Unsubscribe();
                fileEventObservable.Complete();
                Console.ReadKey();
            }
        }
    }
}
