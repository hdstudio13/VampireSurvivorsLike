using Architecture.Input;
using Architecture.TimeManagement;
using Entitas;

namespace Gameplay.Features.Player.Systems
{
    public class PlayerEmitMovementSystem : IExecuteSystem
    {
        private readonly IInputService _input;
        private readonly ITimeService _time;
        private readonly IGroup<GameEntity> _players;

        public PlayerEmitMovementSystem(GameContext context, IInputService input, ITimeService time)
        {
            _input = input;
            _time = time;
            _players = context.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Player, 
                    GameMatcher.Speed)
            );
        }
        
        public void Execute()
        {
            foreach (var player in _players)
            {
                player.ReplaceMoveVector(player.Speed * _input.MoveAxis * _time.DeltaTime);
            }
        }
    }
}