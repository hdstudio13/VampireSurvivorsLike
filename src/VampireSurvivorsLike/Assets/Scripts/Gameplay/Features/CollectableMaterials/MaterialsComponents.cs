using Entitas;

namespace Gameplay.Features.CollectableMaterials
{
    [Game] public class MaterialComponent : IComponent {}
    [Game] public class MaterialTypeComponent : IComponent { public MaterialType Value; }
}