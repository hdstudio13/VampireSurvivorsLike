using System;
using VContainer;

namespace Common
{
    public interface IInstantiator
    {
        T Instantiate<T>();
    }

    public class Instantiator : IInstantiator
    {
        private readonly IObjectResolver _resolver;

        public Instantiator(IObjectResolver resolver)
        {
            _resolver = resolver;
        }

        public T Instantiate<T>()
        {
            var type = typeof(T);

            // 1. Get the constructor (take the first public one)
            var constructor = type.GetConstructors()[0];

            // 2. Get required parameter types
            var parameterInfos = constructor.GetParameters();

            // 3. Resolve each dependency from the container
            var args = new object[parameterInfos.Length];
            for (int i = 0; i < parameterInfos.Length; i++)
            {
                args[i] = _resolver.Resolve(parameterInfos[i].ParameterType);
            }

            // 4. Create the instance
            return (T)Activator.CreateInstance(type, args);
        }
    }
}