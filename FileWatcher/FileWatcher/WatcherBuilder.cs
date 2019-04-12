namespace FileWatcher
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class WatcherBuilder
    {
        private readonly List<FileSystemWatcher> _watchers;

        public WatcherBuilder(IEnumerable<string> sources)
        {
            if (sources == null)
            {
                throw new ArgumentNullException(nameof(sources));
            }

            _watchers = new List<FileSystemWatcher>();

            foreach (var path in sources)
            {
                if (Directory.Exists(path))
                {
                    _watchers.Add(new FileSystemWatcher { Path = path });
                }
            }
        }

        public WatcherBuilder SetNotifyFilter(NotifyFilters filter)
        {
            foreach (var watcher in _watchers)
            {
                watcher.NotifyFilter |= filter;
            }

            return this;
        }

        public WatcherBuilder SetOnCreatedHandler(FileSystemEventHandler onCreated)
        {
            if (onCreated == null)
            {
                throw new ArgumentNullException(nameof(onCreated));
            }

            foreach (var watcher in _watchers)
            {
                watcher.Created += onCreated;
            }

            return this;
        }

        public WatcherBuilder SetOnChangedHandler(FileSystemEventHandler onChanged)
        {
            if (onChanged == null)
            {
                throw new ArgumentNullException(nameof(onChanged));
            }

            foreach (var watcher in _watchers)
            {
                watcher.Changed += onChanged;
            }

            return this;
        }

        public WatcherBuilder SetOnDeletedHandler(FileSystemEventHandler onDeleted)
        {
            if (onDeleted == null)
            {
                throw new ArgumentNullException(nameof(onDeleted));
            }

            foreach (var watcher in _watchers)
            {
                watcher.Deleted += onDeleted;
            }

            return this;
        }

        public WatcherBuilder SetOnRenamedHandler(RenamedEventHandler onRenamed)
        {
            if (onRenamed == null)
            {
                throw new ArgumentNullException(nameof(onRenamed));
            }

            foreach (var watcher in _watchers)
            {
                watcher.Renamed += onRenamed;
            }

            return this;
        }

        public WatcherBuilder IncludeSubdirectories(bool include)
        {
            foreach (var watcher in _watchers)
            {
                watcher.IncludeSubdirectories = include;
            }

            return this;
        }

        public WatcherBuilder StartWatching()
        {
            if (_watchers == null)
            {
                throw new NullReferenceException(nameof(_watchers));
            }

            foreach (var watcher in _watchers)
            {
                watcher.EnableRaisingEvents = true;
            }

            return this;
        }
    }
}
