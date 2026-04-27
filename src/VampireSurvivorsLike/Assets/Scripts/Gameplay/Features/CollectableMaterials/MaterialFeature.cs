using Architecture.Systems;
using Gameplay.Features.CollectableMaterials.Systems;

namespace Gameplay.Features.CollectableMaterials
{
    public class MaterialFeature : Feature
    {
        public MaterialFeature(ISystemFactory factory)
        {
            Add(factory.Create<SpawnMaterialsSystem>());
            Add(factory.Create<AttractNearbyMaterialsToPlayer>());
            Add(factory.Create<DestroyAttractedMaterialsSystem>());
            Add(factory.Create<CullingMaterialsSystem>());
        }
    }
}