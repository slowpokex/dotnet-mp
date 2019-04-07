namespace FileWatcher
{
    using System;
    using System.IO;
    using System.Threading;
    using FileWatcher.Configuration;
    using FileWatcher.Locales;

    public class Watcher
    {
        public static void Main(string[] args)
        {
            Run();
        }

        private static void Run()
        {
            Console.Write(Locale.StartWatch);

            Console.CancelKeyPress += new ConsoleCancelEventHandler(ExitHandler);
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(AppConfigurationProvider.GetCurrentLocale());

            var watcher = new WatcherBuilder(AppConfigurationProvider.GetSourcePaths())

                // Setup notifiers
                .SetNotifyFilter(NotifyFilters.LastAccess)
                .SetNotifyFilter(NotifyFilters.LastWrite)
                .SetNotifyFilter(NotifyFilters.FileName)
                .SetNotifyFilter(NotifyFilters.DirectoryName)

                // Setup handlers
                .SetOnCreatedHandler(OnCreated)
                .SetOnChangedHandler(OnChanged)
                .SetOnRenamedHandler(OnRenamed)
                .SetOnDeletedHandler(OnDeleted);

            while (true){ }
        }

        private static void OnCreated(object source, FileSystemEventArgs e)
        {
        }

        private static void OnChanged(object source, FileSystemEventArgs e)
        {
        }

        private static void OnRenamed(object source, RenamedEventArgs e)
        {
        }

        private static void OnDeleted(object source, FileSystemEventArgs e)
        {
        }

        private static void ExitHandler(object sender, ConsoleCancelEventArgs args)
        {
            Console.Write(Locale.ExitMessage);
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}
