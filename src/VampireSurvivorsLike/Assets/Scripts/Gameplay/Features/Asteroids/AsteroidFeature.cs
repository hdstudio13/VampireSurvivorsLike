using Architecture.Systems;
using Gameplay.Features.Asteroids.Systems;

namespace Gameplay.Features.Asteroids
{
    public class AsteroidFeature : Feature
    {
        public AsteroidFeature(ISystemFactory factory)
        {
            Add(factory.Create<InitializeAsteroidsSystem>());
        }
    }
}