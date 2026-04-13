using System;
using System.Collections.Generic;

namespace HDAttributes.Scripts.Core
{
    public abstract class AttributeHandler
    {
        public abstract void ApplyModifier(AttributeModifier modifier);
        public abstract void RemoveModifier(AttributeModifier modifier);
        public abstract void UpdateModifiers(float deltaTime);
    }

    public class AttributeHandler<TAttribute> : AttributeHandler
        where TAttribute : GameAttribute
    {
        private IAttributeManager _attributeManager;
        private TAttribute _attribute;
        private List<AttributeModifier> _modifiers;
        private List<AttributeModifier> _killQueue;

        public AttributeHandler(IAttributeManager manager, TAttribute attribute)
        {
            _attribute = attribute ?? throw new ArgumentNullException(nameof(attribute), "Attribute cannot be null.");
            _attributeManager = manager ?? throw new ArgumentNullException(nameof(manager), "AttributeManager cannot be null.");
            _modifiers = new List<AttributeModifier>();
            _killQueue = new List<AttributeModifier>();
        }

        public AttributeHandler(IAttributeManager manager, string attribute)
        {
            if (!manager.Has(attribute))
            {
                throw new InvalidOperationException($"Attribute of type {typeof(TAttribute).Name} does not exist in the container.");
            }

            _attribute = manager.Get<TAttribute>(attribute);
            _attributeManager = manager ?? throw new ArgumentNullException(nameof(manager), "AttributeManager cannot be null.");
            _modifiers = new List<AttributeModifier>();
            _killQueue = new List<AttributeModifier>();
        }

        public override void ApplyModifier(AttributeModifier modifier)
        {
            ReleaseKillQueue();

            if (modifier == null)
            {
                throw new ArgumentNullException(nameof(modifier), "Modifier cannot be null.");
            }

            if (_modifiers.Contains(modifier))
            {
                throw new InvalidOperationException("Modifier is already applied to this attribute.");
            }

            modifier.OnKilled += OnModifierKilled;
            _modifiers.Add(modifier);
            modifier.InternalOnApply(_attributeManager, _attribute);
        }

        private void OnModifierKilled(AttributeModifier modifier)
        {
            _killQueue.Add(modifier);
        }

        public override void RemoveModifier(AttributeModifier modifier)
        {
            ReleaseKillQueue();

            if (modifier == null)
            {
                throw new ArgumentNullException(nameof(modifier), "Modifier cannot be null.");
            }

            if (!_modifiers.Contains(modifier))
            {
                throw new InvalidOperationException("Modifier is not applied to this attribute.");
            }

            _modifiers.Remove(modifier);
            modifier.InternalOnRemove();
        }

        public override void UpdateModifiers(float deltaTime)
        {
            foreach (var modifier in _modifiers)
            {
                modifier.InternalOnUpdate(deltaTime);
            }
            ReleaseKillQueue();
        }

        private void ReleaseKillQueue()
        {
            foreach (var modifier in _killQueue)
            {
                _modifiers.Remove(modifier);
            }
            _killQueue.Clear();
        }
    }
}
