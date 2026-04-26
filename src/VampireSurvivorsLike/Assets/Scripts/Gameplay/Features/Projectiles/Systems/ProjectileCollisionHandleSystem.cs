using System.Collections.Generic;
using Entitas;
using Gameplay.Features.Effects.Factory;
using Gameplay.Features.EntityDestroy;

namespace Gameplay.Features.Projectiles.Systems
{
    public class ProjectileCollisionHandleSystem : IExecuteSystem
    {
        private readonly IEffectsFactory _effects;
        private IGroup<GameEntity> _entities;
        private readonly List<GameEntity> _buffer = new(32);

        public ProjectileCollisionHandleSystem(GameContext context, IEffectsFactory effects)
        {
            _effects = effects;
            _entities = context.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Projectile,
                    GameMatcher.TargetEntities,
                    GameMatcher.Damage,
                    GameMatcher.Alive));
        }
        
        public void Execute()
        {
            foreach (var entity in _entities.GetEntities(_buffer))
            {
                if (entity.TargetEntities.Count > 0)
                {
                    foreach (var targetEntity in entity.TargetEntities)
                    {
                        _effects.CreateDamage(entity.Damage, targetEntity.Id, entity.Id);
                    }
                    entity.TargetEntities.Clear();
                    entity.SetDead();
                }
            }
        }
    }
}