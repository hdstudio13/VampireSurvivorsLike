using System;
using Entitas;
using NaughtyAttributes;
using UnityEngine;

namespace Architecture.EntityViews
{
    public abstract class EntityRegistrarBehaviour<TEntity> : MonoBehaviour, IEntityRegistrar<TEntity> where TEntity : Entity
    {
        protected virtual void Reset()
        {
            var entityView = GetComponentInParent<EntityView<TEntity>>();
            entityView?.DebugFindAllRegistrars();
        }

        public abstract void RegisterComponents(TEntity entity);
        public abstract void UnregisterComponents(TEntity entity);
    }
}