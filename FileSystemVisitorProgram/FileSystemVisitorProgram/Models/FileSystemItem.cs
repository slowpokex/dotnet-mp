using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemVisitorProgram.Models
{
    interface FileSystemItem
    {
        string Name { get; set; }
        string Metadata { get; set; }
        int Level { get; set; }
        FileSystemItem[] InnerFileEntries { get; }
    }
}
