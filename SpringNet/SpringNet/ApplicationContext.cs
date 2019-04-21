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
        private readonly Dictionary<string, object> _beans;

        public ApplicationContext ()
        {
            _assembly = Assembly.GetCallingAssembly();
            _configurations = new Dictionary<Type, object>();
            _beans = new Dictionary<string, object>();
        }

        public void Initialize()
        {
            var configurations = _assembly.DefinedTypes
                .Where(x => x.GetCustomAttribute<Configuration>() != null);

            foreach (var c in configurations)
            {
                var instance = Convert.ChangeType(Activator.CreateInstance(c), c);
                _configurations.Add(c, instance);

                var beans = c.GetMethods()
                    .Where(x => !x.GetParameters().Any() && x.GetCustomAttribute<Bean>() != null)
                    .Select(y => new {
                        Name = y.GetCustomAttribute<Bean>().BeanName,
                        Instance = y.Invoke(instance, null)
                    });

                foreach (var bean in beans)
                {
                    _beans.Add(bean.Name, bean.Instance);
                }
            }

            var components = _assembly.DefinedTypes
                .Where(x => x.GetCustomAttribute<Component>() != null)
                .Select(y => new {
                    y.Name,
                    Instance = Convert.ChangeType(Activator.CreateInstance(y), y)
                });

            foreach (var component in components)
            {
                _beans.Add(component.Name, component.Instance);
            }

            var componentBeanFields = _beans
                .Select(x => x.Value.GetType())
                .SelectMany(y => y.GetRuntimeFields())
                .Where(z => z.GetCustomAttribute<Autowired>() != null);

            foreach (var bean in _beans)
            {
                var beanInstance = bean.Value;
                var autowiredFields = beanInstance
                    .GetType()
                    .GetRuntimeFields()
                    .Where(z => z.GetCustomAttribute<Autowired>() != null);

                foreach (var field in autowiredFields)
                {
                    field.SetValue(beanInstance, _beans.GetValueOrDefault(field.Name));
                }
            }

        }

        public object GetBean(string beanName)
        {
            return _beans.GetValueOrDefault(beanName);
        }

        public T GetBean<T>(string beanName)
        {
            var value = GetBean(beanName);
            if (value is T)
            {
                return (T)value;
            }
            try
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch (InvalidCastException)
            {
                return default(T);
            }
        }
    }
}
