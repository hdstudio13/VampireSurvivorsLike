using System;
using HDAttributes.Scripts.Core;

namespace HDAttributes.Scripts.Advanced
{
    public interface IEnumBasedAttributeManager<TEnum> : IAttributeManager where TEnum : System.Enum
    {
        public void Remove(TEnum id);
        public T Get<T>(TEnum id) where T : GameAttribute;
        public GameAttribute Get(TEnum id);
        public bool Has(TEnum id);
        public T GetValue<T>(TEnum id) where T : struct, IComparable;
        public void ApplyModifier<T>(TEnum attributeId, AttributeModifier<T> modifier) where T : GameAttribute;
        public void RemoveModifierFrom(TEnum attributeId, AttributeModifier modifier);
    }
}
