using System;

namespace Entitas.VisualDebuggingFix.Editor
{
    public interface IDefaultInstanceCreator
    {
        bool HandlesType(Type type);

        object CreateDefault(Type type);
    }
}