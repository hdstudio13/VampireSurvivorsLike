using Architecture.EntityViews.GameContext;

namespace Architecture.EntityViews
{
    public interface IGameEntityViewFactory
    {
        GameEntityView Create(string path);
        void Recycle(GameEntityView view);
    }
}