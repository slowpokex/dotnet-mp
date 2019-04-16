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
                var instance = Convert.ChangeType(Activator.CreateInstance(c), c);
                _configurations.Add(c, instance);
                var beans = c.GetMethods().Select(x => x.Invoke(instance, null));
                foreach (var bean in beans)
                {
                    Console.WriteLine(bean);
                }
            }


        }

        public object GetBean(string beanName)
        {
            return null;
        }
    }
}
