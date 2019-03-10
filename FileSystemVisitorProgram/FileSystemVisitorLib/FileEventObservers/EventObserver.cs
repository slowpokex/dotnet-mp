namespace FileSystemVisitorLib.FileEventObservers
{
    using System;

    public class EventObserver: IObserver<Event>
    {
        private IDisposable unsubscriber;

        public virtual void Subscribe(IObservable<Event> provider) => this.unsubscriber = provider.Subscribe(this);

        public virtual void Unsubscribe() => this.unsubscriber.Dispose();

        public bool ShouldInterrupt(string item) => false;

        public bool ShouldSkip(string item) => item.Contains(".git");

        public void OnNext(Event value)
        {
            if (Events.DIRECTORY_FINDED.Equals(value.EventType))
            {
                
            }
        }

        public void OnCompleted()
        {

        }

        public void OnError(Exception error)
        {
            
        }
    }
}
