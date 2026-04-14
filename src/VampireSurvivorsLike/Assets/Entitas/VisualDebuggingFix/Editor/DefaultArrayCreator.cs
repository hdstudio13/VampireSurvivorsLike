using System;

namespace Entitas.VisualDebuggingFix.Editor
{
    public class DefaultArrayCreator : IDefaultInstanceCreator
    {
        public bool HandlesType(Type type) => type.IsArray;

        public object CreateDefault(Type type)
        {
            return (object) Array.CreateInstance(type.GetElementType(), new int[type.GetArrayRank()]);
        }
    }
}