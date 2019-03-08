namespace FileSystemVisitorProgram
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using FileSystemVisitorProgram.Interfaces;
    using FileSystemVisitorProgram.Models;

    public delegate bool FileItemFilter(string pattern);

    public class FileSystemVisitor : IVisitor
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

        public IEnumerable<IFileSystemItem> EnumerateFileSystemItems(string path)
        {
            if (!Directory.Exists(path))
            {
                yield break;
            }

            var directory = Directory.EnumerateFileSystemEntries(path);

            foreach (var file in directory)
            {
                var attr = File.GetAttributes(file);
                IFileSystemItem fileItem;
                if (attr.HasFlag(FileAttributes.Directory))
                {
                    fileItem = new DirectoryItem { Name = file };
                }
                else
                {
                    fileItem = new FileItem { Name = file };
                }
                yield return fileItem;
            }
        }

        public List<IFileSystemItem> GetFileSystemItems()
        {
            if (Directory.Exists(this.filepath))
            {
                var directory = Directory.EnumerateFileSystemEntries(this.filepath);
                foreach (var file in directory)
                {
                    Console.WriteLine(file);
                }
            } else
            {
                return new List<IFileSystemItem>() { };
            }
            return new List<IFileSystemItem>() { };
        }
    } 
}
