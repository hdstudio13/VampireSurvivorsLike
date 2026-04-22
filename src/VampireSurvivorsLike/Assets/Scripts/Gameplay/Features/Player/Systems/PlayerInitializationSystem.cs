using Architecture.Identification;
using Common;
using Entitas;
using UnityEngine;

namespace Gameplay.Features.Player.Systems
{
    public class PlayerInitializationSystem : IInitializeSystem
    {
        private readonly GameContext _context;
        private readonly IIdentifierService _identifier;

        public PlayerInitializationSystem
        (
            GameContext context,
            IIdentifierService identifier
        )
        {
            _context = context;
            _identifier = identifier;
        }
        
        public void Initialize()
        {
            _context
                .CreateEntity()
                .AddId(_identifier.Next())
                .With(x => x.isPlayer = true)
                .AddWorldPosition(new Vector2(0,0))
                .AddMoveSpeed(5)
                .AddViewPath("EntityViews/PlayerView.prefab");
        }
    }
}