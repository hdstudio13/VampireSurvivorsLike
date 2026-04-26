using Entitas;
using Gameplay.Features.Asteroids.Factory;
using UnityEngine;

namespace Gameplay.Features.Asteroids.Systems
{
    public class InitializeAsteroidsSystem : IInitializeSystem 
    {
        private readonly IAsteroidFactory _factory;

        public InitializeAsteroidsSystem(IAsteroidFactory factory)
        {
            _factory = factory;
        }
        
        public void Initialize()
        {
            for (int i = 0; i < 100; i++)
            {
                Vector2 rand = Random.insideUnitCircle * 100;
                Vector3 position = new Vector3(rand.x, 1, rand.y);
                _factory.Create(position);
            }
        }
    }
}