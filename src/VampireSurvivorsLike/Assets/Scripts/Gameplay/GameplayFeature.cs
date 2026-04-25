using Architecture;
using Architecture.Systems;
using Gameplay.Features.Animations;
using Gameplay.Features.Attacking;
using Gameplay.Features.EntityDestroy;
using Gameplay.Features.Movement;
using Gameplay.Features.Player;
using Gameplay.Features.Projectiles;
using Gameplay.Features.Rotation;
using Gameplay.Features.Turrets;
using Gameplay.Features.View;

namespace Gameplay
{
    public class GameplayFeature : Feature
    {
        public GameplayFeature(ISystemFactory factory)
        {
            Add(factory.Create<PlayerFeature>());
            Add(factory.Create<TurretFeature>());
            Add(factory.Create<ProjectileFeature>());
            Add(factory.Create<MovementFeature>());
            Add(factory.Create<RotationFeature>());
            Add(factory.Create<ViewFeature>());
            Add(factory.Create<AnimationFeature>());
            Add(factory.Create<AttackFeature>());
            Add(factory.Create<DestroyFeature>());
        }
    }
}