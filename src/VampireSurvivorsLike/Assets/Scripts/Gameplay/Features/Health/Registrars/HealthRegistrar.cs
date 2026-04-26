using Architecture.EntityViews.GameContext;
using Gameplay.Features.EntityDestroy;
using UnityEngine;

namespace Gameplay.Features.Health.Registrars
{
    public class HealthRegistrar : GameEntityRegistrar
    {
        [SerializeField] private float maxHealth = 100f;
        [SerializeField] private float currentHealth = 100f;
        
        public override void RegisterComponents(GameEntity entity)
        {
            entity
                .AddHealth(maxHealth)
                .AddCurrentHealth(currentHealth)
                .SetAlive();
        }

        public override void UnregisterComponents(GameEntity entity)
        {
            if (entity.hasHealth)
                entity.RemoveHealth();
            if (entity.hasCurrentHealth)
                entity.RemoveCurrentHealth();
        }
    }
}