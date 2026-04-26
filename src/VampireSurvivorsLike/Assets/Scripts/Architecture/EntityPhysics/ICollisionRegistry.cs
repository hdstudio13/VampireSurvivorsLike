namespace Architecture.EntityPhysics
{
    public interface ICollisionRegistry
    {
        void Register(GameEntity entity);
        void Unregister(GameEntity entity);
        GameEntity GetEntity(int id);
    }
}