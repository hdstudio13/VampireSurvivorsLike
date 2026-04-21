using Entitas;

namespace Architecture.Systems
{
    public interface ISystemFactory
    {
        public T Create<T>() where T : ISystem;
    }
}