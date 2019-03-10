using System;
using System.IO;
using FileSystemVisitorLib;
using FileSystemVisitorLib.FileEventObservers;

namespace FileSystemVisitorRunner
{
    internal class Runner
    {
        private static readonly string EntryPath = "D:/Projects";

        private static void Main(string[] args)
        {
            var fileEventObserver = new EventObserver();

            try
            {
                var fileSystemVisitor = new FileSystemVisitor(EntryPath, item => item.Contains("dotnet-mp"));

                fileEventObserver.Subscribe(fileSystemVisitor.GetFileSystemEventObservable());

                foreach (var fileItems in fileSystemVisitor.GetFileItems())
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
                Console.ReadKey();
            }
        }
    }
}
