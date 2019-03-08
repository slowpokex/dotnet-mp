namespace FileSystemVisitorLib.Common
{
    using System;
    using FileSystemVisitorLib.FileEventObservers;

    public class EventObserver: IObserver<Event>
    {
        private IDisposable unsubscriber;

        public virtual void Subscribe(IObservable<Event> provider) => this.unsubscriber = provider.Subscribe(this);

        public virtual void Unsubscribe() => this.unsubscriber.Dispose();

        public void OnNext(Event value)
        {

        }

        public void OnCompleted()
        {

        }

        public void OnError(Exception error)
        {
            
        }
    }
}
