namespace FileSystemVisitorLib
{
    using System.Collections.Generic;

    public interface IVisitor
    {
        IEnumerable<string> GetItems();
    }
}