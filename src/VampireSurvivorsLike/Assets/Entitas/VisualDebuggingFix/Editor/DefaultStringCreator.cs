using System;

namespace Entitas.VisualDebuggingFix.Editor
{
    public class DefaultStringCreator : IDefaultInstanceCreator
    {
        public bool HandlesType(Type type) => type == typeof (string);

        public object CreateDefault(Type type) => (object) string.Empty;
    }
}