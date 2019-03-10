namespace FileSystemVisitorLib.FileEventObservers
{
    using System;
    using System.Collections.Generic;

    public class EventObservable : IObservable<Event>
    {
        private readonly List<IObserver<Event>> observers;

        public EventObservable() => this.observers = new List<IObserver<Event>>();

        public IDisposable Subscribe(IObserver<Event> observer)
        {
            if (!this.observers.Contains(observer))
            {
                this.observers.Add(observer);
            }                

            return new Unsubscriber(this.observers, observer);
        }

        public void NextEvent(Event newEvent)
        {
            foreach (var observer in this.observers)
            {
                observer.OnNext(newEvent);
            }
        }

        public void NextError(Exception e)
        {
            foreach (var observer in this.observers)
            {
                observer.OnError(e);
            }
        }

        public void Complete()
        {
            foreach (var observer in this.observers)
            {
                observer.OnCompleted();
            }
        }

        private class Unsubscriber : IDisposable
        {
            private readonly List<IObserver<Event>> _observers;
            private readonly IObserver<Event> _observer;

            public Unsubscriber(List<IObserver<Event>> observers, IObserver<Event> observer)
            {
                this._observers = observers;
                this._observer = observer;
            }

            public void Dispose()
            {
                if ( this._observer != null)
                {
                    this._observers.Remove(this._observer);
                }    
            }
        }
    }
}
