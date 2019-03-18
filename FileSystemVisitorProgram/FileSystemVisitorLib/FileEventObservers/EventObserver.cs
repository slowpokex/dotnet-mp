namespace FileSystemVisitorLib.FileEventObservers
{
    using System;
    using FileSystemVisitorLib.FileEventObservers.Models;

    public class EventObserver: IEventObserver
    {
        private IDisposable _unsubscriber;

        public void Subscribe(IObservable<Event> provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            _unsubscriber = provider.Subscribe(this);
        }

        public void Unsubscribe()
        {
            _unsubscriber.Dispose();
        }

        public bool ShouldInterrupt(string item)
        {
            return false;
        }

        public bool ShouldSkip(string item)
        {
            return item.Contains(".git");
        }

        public void OnNext(Event value)
        {
            // Provide additional behavior on event
            if (Events.DIRECTORY_FINDED.Equals(value.EventType))
            {
                
            }
        }

        public void OnCompleted()
        {
            // Provide additional behavior on complete
        }

        public void OnError(Exception error)
        {
            // Provide additional behavior on error
        }
    }
}
