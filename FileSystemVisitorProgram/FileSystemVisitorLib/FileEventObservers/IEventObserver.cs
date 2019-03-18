namespace FileSystemVisitorLib.FileEventObservers
{
    using System;
    using FileSystemVisitorLib.FileEventObservers.Models;

    public interface IEventObserver: IObserver<Event>
    {
        void Subscribe(IObservable<Event> provider);
        void Unsubscribe();
        bool ShouldInterrupt(string item);
        bool ShouldSkip(string item);
    }
}