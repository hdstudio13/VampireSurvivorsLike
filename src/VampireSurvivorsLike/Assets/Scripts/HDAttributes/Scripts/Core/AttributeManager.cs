using System;
using System.Collections.Generic;

namespace HDAttributes.Scripts.Core
{
    public class AttributeManager : IAttributeManager
    {
        protected AttributeContainer _container;
        protected Dictionary<string, AttributeHandler> _handlers;

        public AttributeManager()
        {
            _container = new AttributeContainer();
            _handlers = new Dictionary<string, AttributeHandler>();
        }

        public IAttributeContainer Container => _container;

        // container manipulation methods
        public virtual void Add<T>(T attribute) where T : GameAttribute => _container.Add(attribute);
        public virtual void Add<T>(T attribute, params AttributeModifier<T>[] modifiers) where T : GameAttribute
        {
            if (attribute == null)
            {
                throw new ArgumentNullException(nameof(attribute), "Attribute cannot be null.");
            }

            _container.Add(attribute);

            if (modifiers != null && modifiers.Length > 0)
            {
                foreach (var modifier in modifiers)
                {
                    ApplyModifier(attribute.ID, modifier);
                }
            }
        }
        public virtual void Remove(string id) => _container.Remove(id);
        public virtual T Get<T>(string id) where T : GameAttribute => _container.Get<T>(id);
        public virtual GameAttribute Get(string id) => _container.Get(id);
        public virtual bool Has(string id) => _container.Has(id);
        public virtual bool HasAny<T>() where T : GameAttribute => _container.HasAny<T>();
        public virtual T GetValue<T>(string id) where T : struct, IComparable => _container.Get<GameAttribute<T>>(id).Value;
        public virtual void ApplyModifier<T>(string attributeId, AttributeModifier<T> modifier) where T : GameAttribute
        {
            if (modifier == null)
            {
                throw new ArgumentNullException(nameof(modifier), "Modifier cannot be null.");
            }

            if (!_container.Has(attributeId))
            {
                throw new InvalidOperationException($"Attribute of type {typeof(T).Name} does not exist in the container.");
            }

            AttributeHandler handler = null;
            if (_handlers.TryGetValue(attributeId, out AttributeHandler value))
            {
                handler = value;
            }
            else
            {
                var targetAttribute = _container.Get<T>(attributeId);
                handler = new AttributeHandler<T>(this, targetAttribute);
                _handlers[attributeId] = handler;
            }

            handler.ApplyModifier(modifier);
        }
        public virtual void RemoveModifierFrom(string attributeId, AttributeModifier modifier)
        {
            if (modifier == null)
            {
                throw new ArgumentNullException(nameof(modifier), "Modifier cannot be null.");
            }
            if (!_container.Has(attributeId))
            {
                throw new InvalidOperationException($"Attribute {attributeId} does not exist in the container.");
            }

            if (_handlers.TryGetValue(attributeId, out AttributeHandler value))
            {
                value.RemoveModifier(modifier);
            }
            else
            {
                throw new InvalidOperationException($"No handler found for attribute id {attributeId}.");
            }
        }
        public virtual void Update(float deltaTime)
        {
            foreach (var handler in _handlers.Values)
            {
                handler.UpdateModifiers(deltaTime);
            }
        }
    }
}

