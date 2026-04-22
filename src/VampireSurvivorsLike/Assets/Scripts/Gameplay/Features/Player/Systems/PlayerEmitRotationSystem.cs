using Architecture.Input;
using Architecture.TimeManagement;
using Entitas;
using UnityEngine;

namespace Gameplay.Features.Player.Systems
{
    public class PlayerEmitRotationSystem : IExecuteSystem
    {
        private readonly IInputService _input;
        private readonly ITimeService _time;
        private readonly IGroup<GameEntity> _players;

        public PlayerEmitRotationSystem(GameContext context, IInputService input, ITimeService time)
        {
            _input = input;
            _time = time;
            _players = context.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Player, 
                    GameMatcher.RotationSpeed,
                    GameMatcher.WorldRotation)
            );
        }
        
        public void Execute()
        {
            foreach (var player in _players)
            {
                player.ReplaceDeltaRotation(Quaternion.Euler(0, _input.TurnAxis * player.RotationSpeed * _time.DeltaTime, 0));
            }
        }
    }
}