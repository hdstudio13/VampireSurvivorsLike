using System.Collections.Generic;
using Architecture.CameraManagement;
using Entitas;
using Gameplay.Features.EntityDestroy;
using UnityEngine;

namespace Gameplay.Features.Asteroids.Systems
{
    public class AsteroidCullingSystem : IExecuteSystem
    {
        private readonly ICameraService _cameraService;
        private readonly IGroup<GameEntity> _asteroids;
        private readonly IGroup<GameEntity> _players;
        private readonly List<GameEntity> _buffer = new(32);

        public AsteroidCullingSystem(GameContext context, ICameraService cameraService)
        {
            _cameraService = cameraService;
            _asteroids = context.GetGroup(GameMatcher.AllOf(GameMatcher.Asteroid, GameMatcher.Alive, GameMatcher.WorldPosition));
            _players = context.GetGroup(GameMatcher.AllOf(GameMatcher.Player, GameMatcher.WorldPosition));
        }
        
        public void Execute()
        {
            foreach (var asteroid in _asteroids.GetEntities(_buffer))
            {
                if (!_cameraService.IsOnScreen(asteroid.WorldPosition, 0.7f))
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