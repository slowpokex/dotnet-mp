namespace FileSystemVisitorProgram.Interfaces
{
    using FileSystemVisitorProgram.Models;
    using System.Collections.Generic;

    interface IVisitor
    {
        List<FileSystemItem> GetFileSystemItems();
    }
}
