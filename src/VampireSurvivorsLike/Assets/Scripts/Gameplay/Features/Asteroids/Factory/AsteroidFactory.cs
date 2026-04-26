using Architecture.Identification;
using Gameplay.Features.EntityDestroy;
using UnityEngine;

namespace Gameplay.Features.Asteroids.Factory
{
    public class AsteroidFactory : IAsteroidFactory
    {
        private readonly GameContext _context;
        private readonly IIdentifierService _identifier;

        public AsteroidFactory
        (
            GameContext context,
            IIdentifierService identifier
        )
        {
            _context = context;
            _identifier = identifier;
        }
        
        public GameEntity Create(Vector3 position)
        {
            return _context.CreateEntity()
                .AddId(_identifier.Next())
                .AddViewPath("EntityViews/AsteroidView.prefab")
                .AddWorldPosition(position)
                .SetAlive()
                .AddWorldRotation(Quaternion.identity);
        }
    }
}