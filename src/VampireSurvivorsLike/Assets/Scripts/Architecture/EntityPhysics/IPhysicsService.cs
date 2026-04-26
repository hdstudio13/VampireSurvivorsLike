using System.Collections.Generic;
using UnityEngine;

namespace Architecture.EntityPhysics
{
    public interface IPhysicsService
    {
        void SphereOverlap(Vector3 position, float radius, LayerMask mask, in List<GameEntity> output);
    }
}