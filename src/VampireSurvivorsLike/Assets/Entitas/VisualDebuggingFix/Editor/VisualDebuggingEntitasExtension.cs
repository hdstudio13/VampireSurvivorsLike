namespace Entitas.VisualDebuggingFix.Editor
{
    public static class VisualDebuggingEntitasExtension
    {
        public static IEntity CreateEntity2(this IContext context)
        {
            return (IEntity) context.GetType().GetMethod(nameof (CreateEntity2)).Invoke((object) context, (object[]) null);
        }
    }
}