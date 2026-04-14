using System;

namespace Entitas.VisualDebuggingFix.Editor
{
    public interface IComponentDrawer
    {
        bool HandlesType(Type type);

        IComponent DrawComponent(IComponent component);
    }
}