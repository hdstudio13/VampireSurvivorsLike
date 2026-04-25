using Architecture.EntityViews.GameContext;
using UnityEngine;

namespace Gameplay.Features.Turrets
{
    public class TurretAnimator : GameEntityRegistrar
    {
        [SerializeField] private GameEntityView view;
        [SerializeField] private Transform projectileSpawnPivot1;
        [SerializeField] private Transform projectileSpawnPivot2;
        
        public void OnShootAnimationFinished()
        {
            if (view.Entity == null)
                return;

            if (view.Entity.hasProjectilePivot && view.Entity.ProjectilePivot == projectileSpawnPivot1)
                view.Entity.ReplaceProjectilePivot(projectileSpawnPivot2);
            else
                view.Entity.ReplaceProjectilePivot(projectileSpawnPivot1);

            // if (view.Entity.hasAttackCoolDownTimer)
            //     view.Entity.RemoveAttackCoolDownTimer();
        }

        public void OnIdleAnimationStarted()
        {
            if (view.Entity == null)
                return;
            
            view.Entity.ReplaceProjectilePivot(projectileSpawnPivot1);
            
            // if (view.Entity.hasAttackCoolDownTimer)
            //     view.Entity.RemoveAttackCoolDownTimer();
        }

        public override void RegisterComponents(GameEntity entity)
        {
            entity.AddProjectilePivot(projectileSpawnPivot1);
        }

        public override void UnregisterComponents(GameEntity entity)
        {
            if (entity.hasProjectilePivot)
                entity.RemoveProjectilePivot();
        }
    }
}