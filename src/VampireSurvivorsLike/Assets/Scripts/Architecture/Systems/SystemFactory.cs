using Common;
using Entitas;

namespace Architecture.Systems
{
    public class SystemFactory : ISystemFactory
    {
        private readonly IInstantiator _instantiator;

        public SystemFactory(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public T Create<T>() where T : ISystem
        {
            return _instantiator.Instantiate<T>();
        }
    }
}