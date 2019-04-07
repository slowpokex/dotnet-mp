namespace FileWatcher.Models
{
    public class Pattern
    {
        public string Wildcard { get; set; }

        public string Destination { get; set; }

        public bool AddNumber { get; set; }

        public bool AddDate { get; set; }
    }
}
