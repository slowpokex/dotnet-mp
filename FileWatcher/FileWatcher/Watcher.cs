namespace FileWatcher
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Text.RegularExpressions;
    using FileWatcher.Configuration;
    using FileWatcher.Locales;
    using FileWatcher.Models;

    public class Watcher
    {
        private static readonly Random _random = new Random();

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

            var pattern = GetPatternByFile(newFile);

            if (pattern != null)
            {
                Console.WriteLine(Locale.PatternFind, e.FullPath, pattern.Wildcard);
            }

            File.Move(newFile, destinationFilePath);
            Console.WriteLine(Locale.FileMoved, e.FullPath, destinationPath);
        }

        private static Pattern GetPatternByFile(string file)
        {
            var patterns = AppConfigurationProvider.GetPatterns();

            foreach (var pattern in patterns)
            {
                if (Regex.IsMatch(file, pattern.Wildcard, RegexOptions.IgnoreCase))
                {
                    return pattern;
                }
            }

            return null;
        }

        private static string GetParticularFileName(string file)
        {
            var fileName = Path.GetFileNameWithoutExtension(file);
            var fileExt = Path.GetExtension(file);

            var pattern = GetPatternByFile(file);

            if (pattern != null)
            {
                var creationTime = pattern.AddDate ? DateTime.Now.ToString("dd_MMM_yyyy_hh_mm_ss") : "";
                var number = pattern.AddNumber ? _random.Next(int.MaxValue).ToString() : "";
                return string.Concat(number, fileName, creationTime, fileExt);
            }

            return file;

        }

        private static string GetDestinationPath(string file)
        {
            var pattern = GetPatternByFile(file);

            if (pattern != null)
            {
                return !string.IsNullOrEmpty(pattern.Destination) ? pattern.Destination : AppConfigurationProvider.GetDefaultPath();
            }

            return AppConfigurationProvider.GetDefaultPath();
        }

        private static void ExitHandler(object sender, ConsoleCancelEventArgs args)
        {
            Console.Write(Locale.ExitMessage);
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}
