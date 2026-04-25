using Architecture.Input;
using Entitas;

namespace Gameplay.Features.Turrets.Systems
{
    public class EmitAttackInputToPlayerTurretSystem : IExecuteSystem
    {
        private readonly IInputService _input;
        private IGroup<GameEntity> _playerTurrets;

        public EmitAttackInputToPlayerTurretSystem(GameContext context, IInputService input)
        {
            _input = input;
            _playerTurrets = context.GetGroup(GameMatcher
                .AllOf
                (
                    GameMatcher.AbleToAttack,
                    GameMatcher.Player,
                    GameMatcher.Turret
                ));
        }
        
        public void Execute()
        {
            foreach (var turret in _playerTurrets)
            {
                turret.isAttacking = _input.IsAttacking;
            }
        }
    }
}