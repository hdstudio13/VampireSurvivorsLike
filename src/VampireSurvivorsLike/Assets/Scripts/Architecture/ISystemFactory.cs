using Entitas;

namespace Architecture
{
    public interface ISystemFactory
    {
        public T Create<T>() where T : ISystem;
    }
}