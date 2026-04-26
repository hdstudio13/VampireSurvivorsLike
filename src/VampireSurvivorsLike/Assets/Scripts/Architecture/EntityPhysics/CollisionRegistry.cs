using System.Collections.Generic;

namespace Architecture.EntityPhysics
{
    public class CollisionRegistry : ICollisionRegistry
    {
        private readonly Dictionary<int, GameEntity> _registry = new();

        public void Register(GameEntity entity)
        {
            if (entity.hasColliders)
            {
                foreach (var collider in entity.Colliders)
                {
                    _registry.Add(collider.GetInstanceID(), entity);
                }
            }
        }

        public void Unregister(GameEntity entity)
        {
            if (entity.hasColliders)
            {
                foreach (var collider in entity.Colliders)
                {
                    _registry.Remove(collider.GetInstanceID());
                }
            }
        }
        
        public GameEntity GetEntity(int id) => _registry.GetValueOrDefault(id);
    }
}