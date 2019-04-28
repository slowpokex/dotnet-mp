namespace SpringNet.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Class)]
    public class Configuration : Attribute
    {
        public Configuration()
        { }
    }
}
