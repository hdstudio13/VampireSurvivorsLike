using Architecture.TimeManagement;
using Entitas;

namespace Gameplay.Features.Attacking.Systems
{
    public class ProcessAttackCooldownSystem : IExecuteSystem
    {
        private readonly ITimeService _time;
        private readonly IGroup<GameEntity> _attackers;

        public ProcessAttackCooldownSystem(GameContext context, ITimeService time)
        {
            _time = time;
            _attackers = context.GetGroup(GameMatcher.AttackCoolDownTimer);
        }
        
        public void Execute()
        {
            foreach (var attacker in _attackers)
            {
                attacker.ReplaceAttackCoolDownTimer(attacker.AttackCoolDownTimer - _time.DeltaTime);
            }
        }
    }
}