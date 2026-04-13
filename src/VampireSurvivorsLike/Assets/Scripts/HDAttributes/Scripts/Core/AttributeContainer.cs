using System;
using System.Collections.Generic;

namespace HDAttributes.Scripts.Core
{

    public class AttributeContainer : IAttributeContainer
    {
        private Dictionary<string, GameAttribute> _attributes;

        public AttributeContainer()
        {
            _attributes = new Dictionary<string, GameAttribute>();
        }

        public void Add<T>(T attribute) where T : GameAttribute
        {
            if (attribute == null)
                throw new ArgumentNullException(nameof(attribute));

            if (string.IsNullOrEmpty(attribute.ID))
                throw new ArgumentException("Attribute id is null or empty");

            if (!_attributes.ContainsKey(attribute.ID))
            {
                _attributes[attribute.ID] = attribute;
            }
            else
            {
                throw new InvalidOperationException($"Attribute with ID {attribute.ID} already exists.");
            }
        }

        public T Get<T>(string id) where T : GameAttribute
        {
            if (_attributes.TryGetValue(id, out GameAttribute attribute))
            {
                if (attribute is T)
                {
                    return (T)attribute;
                }
                else
                {
                    throw new InvalidCastException($"Failed to cast attribute with id:{id} to type {typeof(T)}");
                }
            }
            else
            {
                throw new KeyNotFoundException($"Attribute with id {id} not found.");
            }
        }

        public GameAttribute Get(string id)
        {
            if (_attributes.TryGetValue(id, out GameAttribute attribute))
            {
                return attribute;
            }
            else
            {
                throw new KeyNotFoundException($"Attribute with id {id} not found.");
            }
        }

        public bool Has(string id)
        {
            return _attributes.ContainsKey(id);
        }

        public void Remove(string id)
        {
            if (_attributes.ContainsKey(id))
            {
                _attributes.Remove(id);
            }
            else
            {
                throw new KeyNotFoundException($"Attribute with id {id} not found.");
            }
        }

        public bool HasAny<T>()
        {
            foreach (var attribute in _attributes.Values)
            {
                if (attribute is T)
                {
                    return true;
                }
            }
            return false;
        }

        public IReadOnlyAttribute<Y> GetReadOnly<T, Y>(string id)
            where T : GameAttribute<Y>
            where Y : struct, IComparable
        {
            return Get<T>(id);
        }

        public IReadOnlyAttribute<Y> GetReadOnly<Y>(string id)
            where Y : struct, IComparable
        {
            return Get<GameAttribute<Y>>(id);
        }
    }
}
