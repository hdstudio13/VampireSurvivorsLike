using Architecture.EntityViews.GameContext;
using UnityEngine;

namespace Architecture.EntityViews.Registrars
{
    public class AnimatorRegistrar : GameEntityRegistrar
    {
        [SerializeField] private Animator animator;

        protected override void Reset()
        {
            base.Reset();
            animator = GetComponentInChildren<Animator>();
        }

        public override void RegisterComponents(GameEntity entity)
        {
            entity.AddAnimator(animator);
        }

        public override void UnregisterComponents(GameEntity entity)
        {
            if (entity.hasAnimator)
                entity.RemoveAnimator();
        }
    }
}