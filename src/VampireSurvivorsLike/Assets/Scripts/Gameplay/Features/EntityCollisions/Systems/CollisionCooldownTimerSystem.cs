using System.Collections.Generic;
using Architecture.TimeManagement;
using Entitas;

namespace Gameplay.Features.EntityCollisions.Systems
{
    public class CollisionCooldownTimerSystem : IExecuteSystem
    {
        private readonly ITimeService _time;
        private readonly IGroup<GameEntity> _cooldowns;
        private readonly List<GameEntity> _buffer = new(32);

        public CollisionCooldownTimerSystem(GameContext context, ITimeService time)
        {
            _time = time;
            _cooldowns = context.GetGroup(GameMatcher.AllOf(GameMatcher.CollisionCooldownTimer));
        }
        
        public void Execute()
        {
            foreach (var cooldown in _cooldowns.GetEntities(_buffer))
            {
                cooldown.ReplaceCollisionCooldownTimer(cooldown.CollisionCooldownTimer - _time.DeltaTime);

                if (cooldown.CollisionCooldownTimer <= 0)
                {
                    cooldown.RemoveCollisionCooldownTimer();
                }
            }
        }
    }
}