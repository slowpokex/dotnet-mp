namespace SpringNet.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Field | AttributeTargets.Property)]
    public class Autowired: Attribute
    {
        public string BeanName { get; set; }

        public bool Required { get; set; }

        public Autowired()
        { }

        public Autowired(string name)
        {
            BeanName = name;
        }

        public Autowired(string name, bool required): this(name)
        {
            Required = required;            
        }
    }
}
