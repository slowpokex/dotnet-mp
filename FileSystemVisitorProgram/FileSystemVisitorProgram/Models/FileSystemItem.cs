using System.Collections.Generic;

namespace FileSystemVisitorProgram.Models
{
    public interface IFileSystemItem
    {
        string Name { get; set; }

        string Metadata { get; set; }

        int Level { get; set; }

        List<IFileSystemItem> InnerFileEntries { get; }
    }
}
