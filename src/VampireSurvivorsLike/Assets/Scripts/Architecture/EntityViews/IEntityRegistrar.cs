using Entitas;

namespace Architecture.EntityViews
{
    public interface IEntityRegistrar<TEntity> where TEntity : IEntity
    {
        void RegisterComponents(TEntity entity);
        void UnregisterComponents(TEntity entity);
    }
}