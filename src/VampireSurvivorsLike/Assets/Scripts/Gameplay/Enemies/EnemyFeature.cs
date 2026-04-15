using Architecture;
using Entitas;

namespace Gameplay.Enemies
{
    public class EnemyFeature : Feature
    {
        public EnemyFeature(ISystemFactory factory)
        {
            Add(factory.Create<SetEnemyMoveDirectionTowardsPlayerSystem>());
        }
    }
}