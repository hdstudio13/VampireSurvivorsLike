using Architecture.EntityViews.GameContext;

namespace Architecture.EntityViews.Registrars
{
    public class TransformRegistrar : GameEntityRegistrar
    {
        public override void RegisterComponents(GameEntity entity)
        {
            entity.AddTransform(transform);
        }

        public override void UnregisterComponents(GameEntity entity)
        {
            if (entity.hasTransform)
                entity.RemoveTransform();
        }
    }
}