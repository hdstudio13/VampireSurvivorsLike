using UnityEngine;

namespace Gameplay.Features.Projectiles.Factory
{
    public interface IProjectileFactory
    {
        GameEntity Create(Vector3 position, Vector3 direction);
    }
}