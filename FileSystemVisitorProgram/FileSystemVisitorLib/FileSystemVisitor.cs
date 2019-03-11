namespace FileSystemVisitorLib
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using FileSystemVisitorLib.FileEventObservers;
    using FileSystemVisitorLib.FileEventObservers.Models;

    public class FileSystemVisitor : IVisitor
    {
        private readonly IEventObservable _fileSystemEventObservable;

        private readonly string _filepath;

        private readonly Predicate<string> _predicate;

        public FileSystemVisitor(string filepath, IEventObservable fileSystemEventObservable)
        {
            if (string.IsNullOrEmpty(filepath) || !Directory.Exists(filepath))
            {
                throw new FileNotFoundException(filepath);
            }

            _fileSystemEventObservable = fileSystemEventObservable;
            _filepath = filepath;
        }

        public FileSystemVisitor(string filepath, IEventObservable fileSystemEventObservable, Predicate<string> predicate)
            : this(filepath, fileSystemEventObservable)
        {
            _predicate = predicate;
        }

        public IEventObservable FileSystemEventObservable => _fileSystemEventObservable;

        public IEnumerable<string> GetItems()
        {
            _fileSystemEventObservable.NextEvent(new Event { EventType = Events.START });
            foreach (var fileItem in GetAllFilesFromDir(_filepath))
            {
                _fileSystemEventObservable.NextEvent(
                    new Event
                    {
                        EventType = Directory.Exists(fileItem) ? Events.DIRECTORY_FINDED : Events.FILE_FINDED,
                        EventData = fileItem
                    });

                if (HasTruePredicate(fileItem))
                {
                    _fileSystemEventObservable.NextEvent(
                        new Event
                        {
                            EventType = Directory.Exists(fileItem) ? Events.FILTERED_DIRECTORY_FINDED : Events.FILTERED_FILE_FINDED,
                            EventData = fileItem
                        });
                }
                if (this._predicate == null || this.HasTruePredicate(fileItem))
                {
                    yield return fileItem;
                }
            }
            _fileSystemEventObservable.NextEvent(new Event { EventType = Events.FINISH });
            _fileSystemEventObservable.Complete();
        }

        private bool HasTruePredicate(string fileItem)
        {
            return _predicate != null && _predicate(fileItem);
        }

        private IEnumerable<string> GetAllFilesFromDir(string dir)
        {
            return Directory.GetFileSystemEntries(dir, "*", SearchOption.AllDirectories);
        }
    } 
}
