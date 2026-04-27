using Architecture;
using Architecture.Identification;
using Architecture.TimeManagement;
using Common;
using Debug;
using Gameplay.Features.CollectableMaterials;
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
            MaterialType material = MaterialType.None;
            float rand = Random.value;
            if (rand < 0.1f)
            {
                material = MaterialType.Gold;
            }
            else if (rand < 0.3f)
            {
                material = MaterialType.Silver;
            }
            
            var viewIndex = Random.Range(1,7);
            return _context.CreateEntity()
                .AddId(_identifier.Next())
                .AddViewPath($"EntityViews/Asteroids/AsteroidView{viewIndex}_{material}.prefab")
                .AddDestroyViewPath("EntityViews/ExplosionView.prefab")
                .AddWorldPosition(position)
                .AddMoveDirection(Random.onUnitCircle.ToTopDown())
                .AddMoveSpeed(Random.Range(0f, 3f))
                .SetAlive()
                .AddHitVFX(HitVFXType.Stone)
                .AddWorldRotation(Quaternion.identity)
                .With(x => x.isCullingCollision = true)
                .With(x => x.isAsteroid = true)
                .With(x => x.AddMaterialType(material), when: (x) => material != MaterialType.None);
        }
    }
}