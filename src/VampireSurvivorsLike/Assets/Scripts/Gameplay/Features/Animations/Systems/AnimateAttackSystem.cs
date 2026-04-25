using Entitas;

namespace Gameplay.Features.Animations.Systems
{
    public class AnimateAttackSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _attackEntities;

        public AnimateAttackSystem(GameContext context)
        {
            _attackEntities = context.GetGroup(GameMatcher.AllOf(GameMatcher.Animator, GameMatcher.AbleToAttack));
        }
        
        public void Execute()
        {
            foreach (var entity in _attackEntities)
            {
                entity.Animator.SetBool(AnimatorHashedStringNames.IsAttacking, entity.isAttacking);
            }
        }
    }
}