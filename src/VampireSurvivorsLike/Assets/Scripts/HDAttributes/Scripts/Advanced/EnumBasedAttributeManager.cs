using System;
using HDAttributes.Scripts.Core;

namespace HDAttributes.Scripts.Advanced
{
    public class EnumBasedAttributeManager<TEnum> : AttributeManager, IEnumBasedAttributeManager<TEnum> where TEnum : System.Enum
    {
        public void ApplyModifier<T>(TEnum attributeId, AttributeModifier<T> modifier) where T : GameAttribute
            => ApplyModifier(attributeId.ToString(), modifier);

        public T Get<T>(TEnum id) where T : GameAttribute => Get<T>(id.ToString());

        public GameAttribute Get(TEnum id) => Get(id.ToString());

        public T GetValue<T>(TEnum id) where T : struct, IComparable
            => GetValue<T>(id.ToString());

        public bool Has(TEnum id)
            => Has(id.ToString());

        public void Remove(TEnum id) => Remove(id.ToString());

        public void RemoveModifierFrom(TEnum attributeId, AttributeModifier modifier)
            => RemoveModifierFrom(attributeId.ToString(), modifier);
    }
}
