namespace FileSystemVisitorLib.FileEventObservers
{
    using System;
    using FileSystemVisitorLib.FileEventObservers.Models;

    public interface IEventObservable: IObservable<Event>
    {
        void NextEvent(Event newEvent);
        void NextError(Exception e);
        void Complete();
    }
}