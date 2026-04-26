using System.Collections.Generic;
using Architecture.TimeManagement;
using Entitas;

namespace Gameplay.Features.EntityDestroy.Systems
{
    public class ProcessDestroyTimerSystem : IExecuteSystem
    {
        private readonly ITimeService _time;
        private readonly IGroup<GameEntity> _delayedDeaths;
        private readonly List<GameEntity> _buffer = new(128);

        public ProcessDestroyTimerSystem(GameContext gameContext, ITimeService time)
        {
            _time = time;
            _delayedDeaths = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.DestroyTimer, 
                    GameMatcher.Alive
                ));
        }
        
        public void Execute()
        {
            foreach (var entity in _delayedDeaths.GetEntities(_buffer))
            {
                entity.ReplaceDestroyTimer(entity.DestroyTimer - _time.DeltaTime);
                if (entity.DestroyTimer <= 0)
                {
                    entity.RemoveDestroyTimer();
                    entity.SetDead();
                }
            }
        }
    }
}