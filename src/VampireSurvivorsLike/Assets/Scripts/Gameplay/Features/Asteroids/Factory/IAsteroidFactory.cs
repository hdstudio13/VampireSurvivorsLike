using UnityEngine;

namespace Gameplay.Features.Asteroids.Factory
{
    public interface IAsteroidFactory
    {
        GameEntity Create(Vector3 position);
    }
}