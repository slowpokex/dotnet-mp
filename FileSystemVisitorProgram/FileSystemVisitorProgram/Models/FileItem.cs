using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemVisitorProgram.Models
{
    internal class FileItem : IFileSystemItem
    {
        public string Name { get; set; }

        public string Metadata { get; set; }

        public List<IFileSystemItem> InnerFileEntries { get; }

        public int Level { get; set; }
    }
}
