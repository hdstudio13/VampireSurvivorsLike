using Architecture;
using Gameplay.Enemies;
using Gameplay.Input;
using Gameplay.Movement;
using Gameplay.Player;
using TimeManagement;

namespace Gameplay
{
    public class GameplayFeature : Feature
    {
        public GameplayFeature(ISystemFactory factory)
        {
            Add(factory.Create<InputFeature>());
            Add(factory.Create<MovementFeature>());
            Add(factory.Create<PlayerFeature>());
            Add(factory.Create<EnemyFeature>());
        }
    }
}