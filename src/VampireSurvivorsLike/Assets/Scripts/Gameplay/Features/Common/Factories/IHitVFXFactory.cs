using UnityEngine;

namespace Gameplay.Features.Common.Factories
{
    public interface IHitVFXFactory
    {
        GameEntity Create(HitVFXType type, Vector3 position, float autoDestroySeconds = 2);
    }
}