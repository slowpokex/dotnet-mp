namespace FileSystemVisitorProgram
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using FileSystemVisitorProgram.Interfaces;
    using FileSystemVisitorProgram.Models;

    public delegate bool FileItemFilter(string pattern);

    class FileSystemVisitor : IVisitor
    {
        private readonly string filepath;
        private readonly FileItemFilter predicate;

        public FileSystemVisitor(string filepath) {
            this.filepath = filepath;
        }

        public FileSystemVisitor(string filepath, FileItemFilter predicate): this(filepath)
        {
            this.predicate = predicate;
        }

        public List<FileSystemItem> GetFileSystemItems()
        {
            if (Directory.Exists(filepath))
            {
                var directory = Directory.EnumerateFileSystemEntries(filepath);
                foreach (var file in directory)
                {
                    Console.WriteLine(file);
                }
            } else
            {
                return new List<FileSystemItem>() { };
            }
            return new List<FileSystemItem>() { };
        }
    } 
}
