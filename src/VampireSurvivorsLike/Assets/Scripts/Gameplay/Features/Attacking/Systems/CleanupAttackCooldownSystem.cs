using System.Collections.Generic;
using Entitas;

namespace Gameplay.Features.Attacking.Systems
{
    public class CleanupAttackCooldownSystem : ICleanupSystem
    {
        private readonly IGroup<GameEntity> _attackers;
        private readonly List<GameEntity> _buffer = new(128);

        public CleanupAttackCooldownSystem(GameContext context)
        {
            _attackers = context.GetGroup(GameMatcher.AttackCoolDownTimer);
        }

        public void Cleanup()
        {
            foreach (var attacker in _attackers.GetEntities(_buffer))
            {
                if (attacker.AttackCoolDownTimer <= 0f)
                    attacker.RemoveAttackCoolDownTimer();
            }
        }
    }
}