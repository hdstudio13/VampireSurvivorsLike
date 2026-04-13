using System;

namespace HDAttributes.Scripts.Core
{
    public class SetModifier<T> : AttributeModifier<GameAttribute<T>> where T : struct, IComparable
    {
        private T _value;

        public SetModifier(T value)
        {
            _value = value;
        }

        public override void OnApply()
        {
            Attribute.SetValue(_value);
            Kill();
        }
    }
}
