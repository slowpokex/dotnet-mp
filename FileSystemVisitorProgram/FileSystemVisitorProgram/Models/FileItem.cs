﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemVisitorProgram.Models
{
    class FileItem : FileSystemItem
    {
        public string Name { get; set; }
        public string Metadata { get; set; }
        public FileSystemItem[] InnerFileEntries { get => new FileSystemItem[] { }; }
    }
}
