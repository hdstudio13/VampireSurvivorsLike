using Architecture.Systems;
using Gameplay.Features.View.Systems;

namespace Gameplay.Features.View
{
    public class ViewFeature : Feature
    {
        public ViewFeature(ISystemFactory factory)
        {
            Add(factory.Create<InstantiateViewFromPathSystem>());
            Add(factory.Create<ViewCleanUpSystem>());
        }
    }
}