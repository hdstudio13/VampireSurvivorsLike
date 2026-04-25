using System.Collections.Generic;
using Entitas;
using Gameplay.Features.Projectiles.Factory;

namespace Gameplay.Features.Attacking.Systems
{
    public class ProcessProjectileAttackSystem : IExecuteSystem
    {
        private readonly IProjectileFactory _projectileFactory;
        private readonly IGroup<GameEntity> _attackers;
        private readonly List<GameEntity> _buffer = new(128);

        public ProcessProjectileAttackSystem(GameContext context, IProjectileFactory projectileFactory)
        {
            _projectileFactory = projectileFactory;
            _attackers = context.GetGroup(GameMatcher
                .AllOf(GameMatcher.Attacking, 
                    GameMatcher.ProjectileAttack, 
                    GameMatcher.ProjectilePivot)
                .NoneOf(GameMatcher.AttackCoolDownTimer));
        }
        
        public void Execute()
        {
            foreach (var attacker in _attackers.GetEntities(_buffer))
            {
                _projectileFactory.Create(attacker.ProjectilePivot.position, attacker.ProjectilePivot.forward);
                if (attacker.hasAttackCoolDown)
                    attacker.ReplaceAttackCoolDownTimer(attacker.AttackCoolDown);
            }
        }
    }
}