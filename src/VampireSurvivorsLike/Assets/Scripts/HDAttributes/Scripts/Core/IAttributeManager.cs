using System;

namespace HDAttributes.Scripts.Core
{
    public interface IAttributeManager
    {
        IAttributeContainer Container { get; }
        void Add<T>(T attribute) where T : GameAttribute;
        void Add<T>(T attribute, params AttributeModifier<T>[] modifiers) where T : GameAttribute;
        void Remove(string id);
        T Get<T>(string id) where T : GameAttribute;
        public T GetValue<T>(string id) where T : struct, IComparable;
        GameAttribute Get(string id);
        bool Has(string id);
        bool HasAny<T>() where T : GameAttribute;
        void ApplyModifier<T>(string attributeId, AttributeModifier<T> modifier) where T : GameAttribute;
        void RemoveModifierFrom(string attributeId, AttributeModifier modifier);
        void Update(float deltaTime);
    }
}

