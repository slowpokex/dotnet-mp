namespace SpringNet.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class Component: Attribute
    {
        public string BeanName { get; set; }

        public bool Required { get; set; }

        public Component()
        { }

        public Component(string name)
        {
            BeanName = name;
        }

        public Component(string name, bool required) : this(name)
        {
            Required = required;
        }
    }
}
