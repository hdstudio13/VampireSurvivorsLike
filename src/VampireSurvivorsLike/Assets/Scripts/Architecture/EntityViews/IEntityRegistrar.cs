using Entitas;

namespace Architecture.EntityViews
{
    public interface IEntityRegistrar<in TEntity> where TEntity : Entity
    {
        void RegisterComponents(TEntity entity);
        void UnregisterComponents(TEntity entity);
    }
}