namespace SpringNet.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Method)]
    public class Bean : Attribute
    {
        public string BeanName { get; set; }

        public Bean()
        { }

        public Bean(string name)
        {
            BeanName = name;
        }
    }
}
