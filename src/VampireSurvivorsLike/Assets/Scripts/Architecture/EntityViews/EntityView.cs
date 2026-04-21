using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;
using NaughtyAttributes;
using UnityEngine;

namespace Architecture.EntityViews
{
    public abstract class EntityView<TEntity> : MonoBehaviour where TEntity : IEntity
    {
        [SerializeField] private EntityRegistrarBehaviour<TEntity>[] monoRegistrars;
        
        private HashSet<IEntityRegistrar<TEntity>> _registrars = new();
        
        public TEntity Entity { get; private set; }

        #region PUBLIC METHODS
        public void AddRegistrar(IEntityRegistrar<TEntity> registrar)
        {
            if (_registrars.Add(registrar))
            {
                if (Entity != null)
                {
                    // register components in case the entity is already setted
                    registrar.RegisterComponents(Entity);
                }
            }
        }
        
        public void SetEntity(TEntity entity)
        {
            if (Entity != null)
                RemoveEntity();
            
            Entity = entity;

            foreach (var registrar in _registrars)
            {
                registrar.RegisterComponents(Entity);
            }
        }
        
        public void RemoveEntity()
        {
            if (Entity == null)
                return;
            
            foreach (var registrar in _registrars)
            {
                registrar.UnregisterComponents(Entity);
            }
            
            Entity = default;
        }

        #endregion
        
        #region UNITY LIFECYCLE

        private void Awake()
        {
            // move monoRegistrars to _registrars
            foreach (var monoRegistrar in monoRegistrars)
            {
                AddRegistrar(monoRegistrar);
            }
        }

        private void OnDestroy()
        {
            if (Entity != null)
            {
                RemoveEntity();
            }
        }
        #endregion
        
        #region DEBUG

        private void Reset()
        {
            DebugFindAllRegistrars();
        }
        
        [Button("Find All Registrars")]
        public void DebugFindAllRegistrars()
        {
            monoRegistrars = GetComponentsInChildren<EntityRegistrarBehaviour<TEntity>>();
        }
        #endregion
    }
}