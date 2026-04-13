using System;
using AYellowpaper.SerializedCollections;
using HDAttributes.Scripts.Core;
using NaughtyAttributes;
using UnityEngine;

namespace HDAttributes.Scripts.Advanced
{
    [Serializable]
    public class EnumAttributeBuilder<TEnum> : IAttributeBuilder where TEnum : Enum
    {
        [SerializeField] private SerializedDictionary<TEnum, AttributeData> _attributes;

        public void BuildAttributes(IAttributeManager manager)
        {
            foreach (var attributeData in _attributes)
                switch (attributeData.Value.Type)
                {
                    case AttributeType.Int:
                        manager.Add(GameAttribute<int>.Create(attributeData.Key, attributeData.Value.IntValues.x,
                            attributeData.Value.IntValues.y, attributeData.Value.IntValues.z));
                        break;
                    case AttributeType.Float:
                        manager.Add(GameAttribute<float>.Create(attributeData.Key, attributeData.Value.FloatValues.x,
                            attributeData.Value.FloatValues.y, attributeData.Value.FloatValues.z));
                        break;
                    case AttributeType.Bool:
                        manager.Add(GameAttribute<bool>.Create(attributeData.Key, attributeData.Value.BoolValue, false,
                            true));
                        break;
                }
        }

        [Serializable]
        private struct AttributeData
        {
            public AttributeType Type;

            private bool IsFloat => Type == AttributeType.Float;
            private bool IsInt => Type == AttributeType.Int;
            private bool IsBool => Type == AttributeType.Bool;

            [AllowNesting] [ShowIf("IsFloat")] [Label("Initial / Min / Max")]
            public Vector3 FloatValues;

            [AllowNesting] [ShowIf("IsInt")] [Label("Initial / Min / Max")]
            public Vector3Int IntValues;

            [AllowNesting] [ShowIf("IsBool")] [Label("Initial Value")]
            public bool BoolValue;
        }

        private enum AttributeType
        {
            Int,
            Float,
            Bool
        }
    }
}