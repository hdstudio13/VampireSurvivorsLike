using System.Collections.Generic;
using Architecture;
using Entitas;
using Gameplay.Features.CollectableMaterials.Factories;
using UnityEngine;

namespace Gameplay.Features.CollectableMaterials.Systems
{
    public class SpawnMaterialsSystem : IExecuteSystem
    {
        private readonly IMaterialFactory _materialFactory;
        private readonly IGroup<GameEntity> _asteroids;
        private readonly List<GameEntity> _buffer = new(8);

        public SpawnMaterialsSystem(GameContext context, IMaterialFactory materialFactory)
        {
            _materialFactory = materialFactory;
            _asteroids = context.GetGroup(GameMatcher
                .AllOf(GameMatcher.Asteroid, GameMatcher.Destroying, GameMatcher.MaterialType));
        }
        
        public void Execute()
        {
            foreach (var asteroid in _asteroids.GetEntities(_buffer))
            {
                for (int i = 0; i < 10; i++)
                {
                    _materialFactory.Create(asteroid.MaterialType, asteroid.WorldPosition + (Random.insideUnitCircle * 1).ToTopDown());
                }

                asteroid.RemoveMaterialType();
            }
        }
    }
}