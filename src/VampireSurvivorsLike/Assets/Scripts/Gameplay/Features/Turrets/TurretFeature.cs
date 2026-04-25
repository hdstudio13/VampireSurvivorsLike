using Architecture.Systems;
using Gameplay.Features.Turrets.Systems;

namespace Gameplay.Features.Turrets
{
    public class TurretFeature : Feature
    {
        public TurretFeature(ISystemFactory factory)
        {
            Add(factory.Create<SetPlayerTurretRotationSystem>());
            Add(factory.Create<EmitAttackInputToPlayerTurretSystem>());
        }
    }
}