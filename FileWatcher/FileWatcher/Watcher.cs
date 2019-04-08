namespace FileWatcher
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Text.RegularExpressions;
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
            CultureInfo.CurrentCulture = new CultureInfo(AppConfigurationProvider.GetCurrentLocale());
            Console.CancelKeyPress += new ConsoleCancelEventHandler(ExitHandler);

            try
            {
                var watcher = new WatcherBuilder(AppConfigurationProvider.GetSourcePaths())
                    // Setup notifiers
                    .SetNotifyFilter(NotifyFilters.LastAccess)
                    .SetNotifyFilter(NotifyFilters.LastWrite)
                    .SetNotifyFilter(NotifyFilters.FileName)
                    .SetNotifyFilter(NotifyFilters.DirectoryName)

                    // With Subdirectories
                    .IncludeSubdirectories(true)

                    // Setup handlers
                    .SetOnCreatedHandler(OnCreated)

                    // Start
                    .StartWatching();

                Console.WriteLine(Locale.StartWatch);
                while (true) { }
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(Locale.WrongReference, e.Message);
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(Locale.WrongParams, e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(Locale.WrongParams, e.Message);
            }
            finally
            {
                Console.ReadKey();
            }
        }

        private static void OnCreated(object source, FileSystemEventArgs e)
        {
            var newFile = e.FullPath;
            var creationTime = File.GetCreationTime(e.FullPath);
            var destinationPath = GetDestinationPath(newFile);
            var destinationFilePath = Path.Combine(destinationPath, GetParticularFileName(Path.GetFileName(newFile)));

            Console.WriteLine(Locale.OnCreated, e.FullPath, creationTime.ToString("dd MMM yyyy hh:mm:ss"));

            if (!Directory.Exists(destinationPath))
            {
                Directory.CreateDirectory(destinationPath);
            }

            if (File.Exists(destinationFilePath))
            {
                File.Delete(destinationFilePath);
            }

            File.Move(newFile, destinationFilePath);
        }

        private static string GetParticularFileName(string file)
        {
            var patterns = AppConfigurationProvider.GetPatterns();
            var fileName = Path.GetFileNameWithoutExtension(file);
            var fileExt = Path.GetExtension(file);

            foreach (var pattern in patterns)
            {
                if (Regex.IsMatch(file, pattern.Wildcard, RegexOptions.IgnoreCase))
                {
                    var creationTime = pattern.AddDate ? DateTime.Now.ToString("dd_MMM_yyyy_hh:mm:ss") : "";
                    // var number = pattern.AddNumber ? Random
                    return string.Concat(fileName, creationTime, fileExt);
                }
            }

            return file;

        }

        private static string GetDestinationPath(string file)
        {
            var patterns = AppConfigurationProvider.GetPatterns();

            foreach (var pattern in patterns)
            {
                if (Regex.IsMatch(file, pattern.Wildcard, RegexOptions.IgnoreCase))
                {
                    return pattern.Destination ?? AppConfigurationProvider.GetDefaultPath();
                }
            }

            return AppConfigurationProvider.GetDefaultPath();
        }

        private static void MoveFile(string sourcePath, string destinationPath)
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
