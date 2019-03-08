namespace FileSystemVisitorLib.FileEventObservers
{
    using FileSystemVisitorLib.Enums;

    public class Event
    {
        public Events EventType { get; set; }

        public string EventData { get; set; }
    }
}
