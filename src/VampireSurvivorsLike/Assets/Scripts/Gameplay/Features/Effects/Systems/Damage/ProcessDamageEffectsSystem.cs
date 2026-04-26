using System.Collections.Generic;
using Debug;
using Entitas;
using Gameplay.Features.EntityDestroy;

namespace Gameplay.Features.Effects.Systems.Damage
{
    public class ProcessDamageEffectsSystem : IExecuteSystem
    {
        private readonly GameContext _context;
        private readonly IGroup<GameEntity> _effects;

        public ProcessDamageEffectsSystem(GameContext context)
        {
            _context = context;
            _effects = context.GetGroup(GameMatcher.AllOf(
                GameMatcher.Effect,
                GameMatcher.Target,
                GameMatcher.Damage,
                GameMatcher.Alive));
        }
        
        public void Execute()
        {
            foreach (var effect in _effects)
            {
                var target = _context.GetEntityWithId(effect.Target);
                if (target.isAlive && target.hasCurrentHealth)
                {
                    target.ReplaceCurrentHealth(target.CurrentHealth - effect.Damage);
                }
            }
        }
    }
}