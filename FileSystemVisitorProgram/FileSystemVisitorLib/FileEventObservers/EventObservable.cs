namespace FileSystemVisitorLib.Common
{
    using System;
    using System.Collections.Generic;
    using FileSystemVisitorLib.FileEventObservers;

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
