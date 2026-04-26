using System.Collections.Generic;
using System.Linq;
using Architecture.EntityPhysics;
using Architecture.EntityViews.GameContext;
using UnityEngine;
using VContainer;

namespace Architecture.EntityViews.Registrars
{
    public class CollidersRegistrar : GameEntityRegistrar
    {
        [SerializeField] private List<Collider> colliders;
        private ICollisionRegistry _registry;

        [Inject]
        private void Construct(ICollisionRegistry registry)
        {
            _registry = registry;
        }
        
        protected override void Reset()
        {
            colliders = GetComponentsInChildren<Collider>().ToList();
            base.Reset();
        }

        public override void RegisterComponents(GameEntity entity)
        {
            entity.AddColliders(colliders);
            _registry.Register(entity);
        }

        public override void UnregisterComponents(GameEntity entity)
        {
            if (entity.hasColliders)
                entity.RemoveColliders();
            _registry.Unregister(entity);
        }
    }
}