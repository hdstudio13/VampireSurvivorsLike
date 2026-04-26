using Architecture;
using Architecture.Identification;
using Architecture.TimeManagement;
using Common;
using Debug;
using Gameplay.Features.Common.Factories;
using Gameplay.Features.EntityDestroy;
using UnityEngine;

namespace Gameplay.Features.Asteroids.Factory
{
    public class AsteroidFactory : IAsteroidFactory
    {
        private readonly GameContext _context;
        private readonly IIdentifierService _identifier;
        private readonly Camera _camera;

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
            var viewIndex = Random.Range(1,7);
            return _context.CreateEntity()
                .AddId(_identifier.Next())
                .AddViewPath($"EntityViews/Asteroids/AsteroidView{viewIndex}.prefab")
                .AddDestroyViewPath("EntityViews/ExplosionView.prefab")
                .AddWorldPosition(position)
                .AddMoveDirection(Random.onUnitCircle.ToTopDown())
                .AddMoveSpeed(Random.Range(0f, 3f))
                .SetAlive()
                .AddHitVFX(HitVFXType.Stone)
                .AddWorldRotation(Quaternion.identity)
                .With(x => x.isAsteroid = true);
        }
    }
}