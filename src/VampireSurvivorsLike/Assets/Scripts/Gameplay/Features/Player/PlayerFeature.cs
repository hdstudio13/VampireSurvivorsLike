using Architecture.Systems;
using Gameplay.Features.Player.Systems;
using UnityEngine;

namespace Gameplay.Features.Player
{
    public class PlayerFeature : Feature
    {
        public PlayerFeature(ISystemFactory systemFactory)
        {
            Add(systemFactory.Create<PlayerInitializationSystem>());
            Add(systemFactory.Create<PlayerCameraInitializationSystem>());
            Add(systemFactory.Create<PlayerEmitMovementSystem>());
            Add(systemFactory.Create<PlayerEmitRotationSystem>());
        }
    }
}