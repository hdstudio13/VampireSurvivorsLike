using Architecture.Input;
using Architecture.TimeManagement;
using Entitas;
using UnityEngine;

namespace Gameplay.Features.Player.Systems
{
    public class PlayerEmitMovementSystem : IExecuteSystem
    {
        private readonly IInputService _input;
        private readonly IGroup<GameEntity> _players;

        public PlayerEmitMovementSystem(GameContext context, IInputService input)
        {
            _input = input;
            _players = context.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Player, 
                    GameMatcher.MoveSpeed,
                    GameMatcher.WorldRotation)
            );
        }
        
        public void Execute()
        {
            foreach (var player in _players)
            {
                player.ReplaceMoveDirection((player.WorldRotation * Vector3.forward).normalized);
                player.isMoving = _input.GasAxis > 0f;
            }
        }
    }
}