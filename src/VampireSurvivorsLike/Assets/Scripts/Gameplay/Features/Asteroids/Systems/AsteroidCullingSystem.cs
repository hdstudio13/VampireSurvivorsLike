using System.Collections.Generic;
using Entitas;
using Gameplay.Features.EntityDestroy;
using UnityEngine;

namespace Gameplay.Features.Asteroids.Systems
{
    public class AsteroidCullingSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _asteroids;
        private readonly IGroup<GameEntity> _players;
        private readonly List<GameEntity> _buffer = new(32);

        public AsteroidCullingSystem(GameContext context)
        {
            _asteroids = context.GetGroup(GameMatcher.AllOf(GameMatcher.Asteroid, GameMatcher.Alive, GameMatcher.WorldPosition));
            _players = context.GetGroup(GameMatcher.AllOf(GameMatcher.Player, GameMatcher.WorldPosition));
        }
        
        public void Execute()
        {
            var players = _players.GetEntities();
            Vector3 centerOfMass = Vector3.zero;
            foreach (var entity in players)
            {
                centerOfMass += entity.WorldPosition;
            }
            centerOfMass /= players.Length;

            foreach (var asteroid in _asteroids.GetEntities(_buffer))
            {
                if (Vector3.Distance(asteroid.WorldPosition, centerOfMass) >= Constants.RELEASE_DISTANCE)
                {
                    // make it silently destroy
                    if (asteroid.hasDestroyViewPath)
                        asteroid.RemoveDestroyViewPath();
                    
                    asteroid.SetDead();
                }
            }
        }
    }
}