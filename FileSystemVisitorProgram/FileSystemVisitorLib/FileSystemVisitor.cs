namespace FileSystemVisitorLib
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using FileSystemVisitorLib.FileEventObservers;

    public class FileSystemVisitor
    {
        private readonly EventObservable fileSystemEventObservable = new EventObservable();

        private readonly string filepath;

        private readonly Predicate<string> predicate;

        public FileSystemVisitor(string filepath) {
            if (filepath == null || !Directory.Exists(filepath))
            {
                throw new FileNotFoundException(filepath);
            }
            this.filepath = filepath;
        }

        public FileSystemVisitor(string filepath, Predicate<string> predicate) : this(filepath) => this.predicate = predicate;

        public EventObservable GetFileSystemEventObservable() => this.fileSystemEventObservable;

        public IEnumerable<string> GetFileItems()
        {
            this.fileSystemEventObservable.NextEvent(new Event { EventType = Events.START });
            foreach (var fileItem in this.GetAllFilesFromDir(this.filepath))
            {
                this.fileSystemEventObservable.NextEvent(new Event
                {
                    EventType = Directory.Exists(fileItem) ? Events.DIRECTORY_FINDED : Events.FILE_FINDED,
                    EventData = fileItem
                });

                if (this.HasTruePredicate(fileItem))
                {
                    this.fileSystemEventObservable.NextEvent(new Event
                    {
                        EventType = Directory.Exists(fileItem) ? Events.FILTERED_DIRECTORY_FINDED : Events.FILTERED_FILE_FINDED,
                        EventData = fileItem
                    });
                }
                if (this.predicate == null || this.HasTruePredicate(fileItem))
                {
                    yield return fileItem;
                }
            }
            this.fileSystemEventObservable.NextEvent(new Event { EventType = Events.FINISH });
            this.fileSystemEventObservable.Complete();
        }

        public bool HasTruePredicate(string fileItem) => this.predicate != null && this.predicate(fileItem);

        public IEnumerable<string> GetAllFilesFromDir(string dir) => Directory.GetFileSystemEntries(dir, "*", SearchOption.AllDirectories);
    } 
}
