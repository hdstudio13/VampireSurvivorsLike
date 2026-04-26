using System.Collections.Generic;
using UnityEngine;

namespace Architecture.EntityPhysics
{
    public class PhysicsService : IPhysicsService
    {
        private readonly ICollisionRegistry _collisionRegistry;
        private readonly Collider[] _buffer = new Collider[32];
        
        public PhysicsService(ICollisionRegistry collisionRegistry)
        {
            _collisionRegistry = collisionRegistry;
        }
        
        public void SphereOverlap(Vector3 position, float radius, LayerMask mask, in List<GameEntity> output)
        {
            var count = Physics.OverlapSphereNonAlloc(position, radius, _buffer, mask);
            output.Clear();
            if (count > 0)
            {
                for (int i = 0;i < count;i++)
                    output.Add(_collisionRegistry.GetEntity(_buffer[i].GetInstanceID()));
            }
        }
    }
}