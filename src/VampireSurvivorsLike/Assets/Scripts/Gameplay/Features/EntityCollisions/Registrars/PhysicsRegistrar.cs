using System;
using System.Collections.Generic;
using Architecture.EntityPhysics;
using Architecture.EntityViews.GameContext;
using Common;
using NaughtyAttributes;
using UnityEngine;
using VContainer;

namespace Gameplay.Features.EntityCollisions.Registrars
{
    public class PhysicsRegistrar : GameEntityRegistrar
    {
        [SerializeField] private bool canBeCollided;
        [SerializeField, ShowIf(nameof(canBeCollided))] private List<Collider> colliders;
        [SerializeField] private bool canDetectCollisions;
        [SerializeField, ShowIf(nameof(canDetectCollisions))] private float collisionCheckRadius;
        [SerializeField, ShowIf(nameof(canDetectCollisions))] private LayerMask layerMask;
        [SerializeField, ShowIf(nameof(canDetectCollisions))] private bool drawGizmos;
        private ICollisionRegistry _registry;

        protected override void Reset()
        {
            colliders = new List<Collider>(GetComponentsInChildren<Collider>());
            base.Reset();
        }

        private void OnDrawGizmos()
        {
            if (canDetectCollisions && drawGizmos)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, collisionCheckRadius);
            }
        }

        [Inject]
        private void Construct(ICollisionRegistry registry)
        {
            _registry = registry;
        }
        
        public override void RegisterComponents(GameEntity entity)
        {
            if (canBeCollided)
            {
                entity.AddColliders(colliders);
                _registry.Register(entity);
            }

            if (canDetectCollisions)
            {
                entity
                    .AddRadius(collisionCheckRadius)
                    .AddLayerMask(layerMask)
                    .AddTargetEntities(new List<GameEntity>())
                    .With(x => x.isPerformingCollisionCheck = true);
            }
        }

        public override void UnregisterComponents(GameEntity entity)
        {
            if (entity.hasColliders)
            {
                _registry.Unregister(entity);
                entity.RemoveColliders();
            }
            
            if (entity.hasRadius)
                entity.RemoveRadius();
            if (entity.hasTargetEntities)
                entity.RemoveTargetEntities();
            if (entity.hasLayerMask)
                entity.RemoveLayerMask();
            entity.isPerformingCollisionCheck = false;
        }
    }
}