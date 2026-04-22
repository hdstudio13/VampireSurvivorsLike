using Architecture.Configs;
using Architecture.Identification;
using Common;
using Entitas;
using Gameplay.Player;
using UnityEngine;

namespace Gameplay.Features.Player.Systems
{
    public class PlayerInitializationSystem : IInitializeSystem
    {
        private readonly GameContext _context;
        private readonly IIdentifierService _identifier;
        private readonly IConfigProvider _configProvider;

        public PlayerInitializationSystem
        (
            GameContext context,
            IIdentifierService identifier,
            IConfigProvider configProvider
        )
        {
            _context = context;
            _identifier = identifier;
            _configProvider = configProvider;
        }
        
        public void Initialize()
        {
            var playerConfig = _configProvider.Get<PlayerConfig>();

            _context
                .CreateEntity()
                .AddId(_identifier.Next())
                .With(x => x.isPlayer = true)
                .AddWorldPosition(new Vector2(0, 0))
                .AddMoveSpeed(playerConfig.MoveSpeed)
                .AddViewPath(playerConfig.PlayerPrefab.RuntimeKey.ToString())
                .AddRotationSpeed(playerConfig.RotationSpeed)
                .AddWorldRotation(Quaternion.identity)
                .AddAcceleration(playerConfig.Acceleration)
                .AddDeacceleration(playerConfig.Deacceleration);
        }
    }
}