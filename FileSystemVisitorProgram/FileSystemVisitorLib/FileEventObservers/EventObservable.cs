namespace FileSystemVisitorLib.FileEventObservers
{
    using System;
    using System.Collections.Generic;
    using FileSystemVisitorLib.FileEventObservers.Models;

    public class EventObservable : IEventObservable
    {
        private readonly List<IObserver<Event>> _observers;

        public EventObservable()
        {
            _observers = new List<IObserver<Event>>();
        }

        public IDisposable Subscribe(IObserver<Event> observer)
        {
            if (observer == null)
            {
                throw new ArgumentNullException(nameof(observer));
            }

            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }                

            return new Unsubscriber(_observers, observer);
        }

        public void NextEvent(Event newEvent)
        {
            foreach (var observer in _observers)
            {
                observer.OnNext(newEvent);
            }
        }

        public void NextError(Exception e)
        {
            foreach (var observer in _observers)
            {
                observer.OnError(e);
            }
        }

        public void Complete()
        {
            foreach (var observer in _observers)
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
                _observers = observers;
                _observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null)
                {
                    _observers.Remove(_observer);
                }    
            }
        }
    }
}
