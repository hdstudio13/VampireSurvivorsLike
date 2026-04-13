using System;

namespace HDAttributes.Scripts.Core
{
    public interface IReadOnlyAttribute<T> where T : struct, IComparable
    {
        public T Value { get; }
        public T MaxValue { get; }
        public T MinValue { get; }

        public event Action<T /*last value*/, T /*current value*/> OnMinimumReached;
        public event Action<T /*last value*/, T /*current value*/> OnMaxReached;
        public event Action<T /*last value*/, T /*current value*/> OnValueChanged;
        public event Action<T /*last value*/, T /*current value*/> OnMaxValueChanged;
        public event Action<T /*last value*/, T /*current value*/> OnMinValueChanged;
    }
}
