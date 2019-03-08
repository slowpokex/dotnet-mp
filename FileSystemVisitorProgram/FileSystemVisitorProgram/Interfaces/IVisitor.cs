namespace FileSystemVisitorProgram.Interfaces
{
    using FileSystemVisitorProgram.Models;
    using System.Collections.Generic;

    public interface IVisitor
    {
        List<IFileSystemItem> GetFileSystemItems();
    }
}
