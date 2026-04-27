using UnityEngine;

namespace Gameplay.Features.CollectableMaterials.Factories
{
    public interface IMaterialFactory
    {
        GameEntity Create(MaterialType type, Vector3 worldPosition);
    }
}