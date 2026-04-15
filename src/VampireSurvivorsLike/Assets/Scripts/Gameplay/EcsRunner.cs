using System;
using Architecture;
using Gameplay.Input;
using TimeManagement;
using UnityEngine;
using VContainer;

namespace Gameplay
{
    public class EcsRunner : MonoBehaviour
    {
        private ISystemFactory _systems;
        private GameplayFeature _gameplayFeature;

        [Inject]
        private void Construct
        (
            ISystemFactory systems
        )
        {
            _systems = systems;
        }
        
        private void Awake()
        {
            _gameplayFeature = _systems.Create<GameplayFeature>();
        }

        private void Start()
        {
            _gameplayFeature.Initialize();
        }

        private void Update()
        {
            _gameplayFeature.Execute();
            _gameplayFeature.Cleanup();
        }

        private void OnDestroy()
        {
            _gameplayFeature.TearDown();
        }
    }
}