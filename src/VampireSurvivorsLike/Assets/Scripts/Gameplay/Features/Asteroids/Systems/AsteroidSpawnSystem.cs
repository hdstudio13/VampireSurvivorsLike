using System.Collections.Generic;
using System.Linq;
using Architecture;
using Debug;
using Entitas;
using Gameplay.Features.Asteroids.Factory;
using UnityEngine;

namespace Gameplay.Features.Asteroids.Systems
{
    public class AsteroidSpawnSystem : IExecuteSystem
    {
        private readonly IAsteroidFactory _factory;
        private readonly IGroup<GameEntity> _asteroids;
        private readonly IGroup<GameEntity> _players;

        public AsteroidSpawnSystem(GameContext context, IAsteroidFactory factory)
        {
            _factory = factory;
            _asteroids = context.GetGroup(GameMatcher.AllOf(GameMatcher.Asteroid, GameMatcher.Alive));
            _players = context.GetGroup(GameMatcher.AllOf(GameMatcher.Player, GameMatcher.WorldPosition));
        }
        
        public void Execute()
        {
            int diff = Constants.TARGET_ASTEROIDS_COUNT - _asteroids.GetEntities().Length;
            if (diff > 0)
            {
                var players = _players.GetEntities();
                Vector3 centerOfMass = Vector3.zero;
                foreach (var entity in players)
                {
                    centerOfMass += entity.WorldPosition;
                }
                centerOfMass /= players.Length;
                
                for (int i = 0; i < diff; i++)
                {
                    Vector3 rand = Random.insideUnitCircle.ToTopDown();
                    Vector3 targetPosition = rand.normalized * Constants.MIN_DISTANCE_FROM_PLAYER + rand * (Constants.MAX_DISTANCE_FROM_PLAYER - Constants.MIN_DISTANCE_FROM_PLAYER) + centerOfMass;
                    targetPosition += Vector3.up * 0.75f;
                    _factory.Create(targetPosition);
                }
            }
        }
    }
}