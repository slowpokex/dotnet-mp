namespace SpringNet
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using SpringNet.Attributes;

    public class ApplicationContext
    {
        private readonly Assembly _assembly;
        private readonly Dictionary<Type, object> _configurations;
        private readonly Dictionary<Type, object> _beans;

        public ApplicationContext ()
        {
            _assembly = Assembly.GetCallingAssembly();
            _configurations = new Dictionary<Type, object>();
            _beans = new Dictionary<Type, object>();
        }

        public void Initialize()
        {
            var configurations = _assembly.DefinedTypes
                .Where(x => x.GetCustomAttribute(typeof(Configuration)) != null);

            foreach (var c in configurations)
            {
                _configurations.Add(c, Activator.CreateInstance(c));
            }
        }

        public object GetBean(string beanName)
        {
            return null;
        }
    }
}
