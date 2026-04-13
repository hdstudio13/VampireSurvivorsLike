using System;

namespace HDAttributes.Scripts.Core
{
    public class GameAttribute
    {
        public string ID { get; private set; }

        public GameAttribute(string id)
        {
            ID = id;
        }
    }

    public class GameAttribute<T> : GameAttribute, IReadOnlyAttribute<T> where T : struct, IComparable
    {
        public T Value { get; private set; }
        public T MaxValue { get; private set; }
        public T MinValue { get; private set; }

        public event Action<T /*last value*/, T /*current value*/> OnMinimumReached;
        public event Action<T /*last value*/, T /*current value*/> OnMaxReached;
        public event Action<T /*last value*/, T /*current value*/> OnValueChanged;
        public event Action<T /*last value*/, T /*current value*/> OnMaxValueChanged;
        public event Action<T /*last value*/, T /*current value*/> OnMinValueChanged;

        public GameAttribute(string id, T value, T minValue, T maxValue) : base(id)
        {
            if (minValue.CompareTo(maxValue) > 0)
            {
                throw new ArgumentException("Min value cannot be greater than max value.");
            }

            if (value.CompareTo(minValue) < 0 || value.CompareTo(maxValue) > 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Value must be within the range defined by min and max values.");
            }

            MinValue = minValue;
            MaxValue = maxValue;
            Value = value;
        }

        public static GameAttribute<T> Create<TEnum>(TEnum id, T value, T minValue, T maxValue)
        {
            return new GameAttribute<T>(id.ToString(), value, minValue, maxValue);
        }

        public static GameAttribute<T> Create(string id, T value, T minValue, T maxValue)
        {
            return new GameAttribute<T>(id.ToString(), value, minValue, maxValue);
        }

        public void SetValue(T value)
        {
            T lastValue = Value;
            Value = value;

            if (value.CompareTo(MinValue) <= 0)
            {
                Value = MinValue;
                OnMinimumReached?.Invoke(lastValue, Value);
            }

            else if (value.CompareTo(MaxValue) >= 0)
            {
                Value = MaxValue;
                OnMaxReached?.Invoke(lastValue, Value);
            }

            OnValueChanged?.Invoke(lastValue, Value);
        }

        public void SetMaxValue(T maxValue)
        {
            if (maxValue.CompareTo(MinValue) < 0)
            {
                throw new ArgumentException("Max value cannot be less than min value.");
            }

            MaxValue = maxValue;
            OnMaxValueChanged?.Invoke(Value, MaxValue);
            if (Value.CompareTo(MaxValue) > 0)
            {
                SetValue(MaxValue);
            }
        }

        public void SetMinValue(T minValue)
        {
            if (minValue.CompareTo(MaxValue) > 0)
            {
                throw new ArgumentException("Min value cannot be greater than max value.");
            }

            MinValue = minValue;
            OnMinValueChanged?.Invoke(Value, MinValue);
            if (Value.CompareTo(MinValue) < 0)
            {
                SetValue(MinValue);
            }
        }
    }
}
