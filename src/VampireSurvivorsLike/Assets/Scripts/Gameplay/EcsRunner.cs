using System;
using Gameplay.Input;
using TimeManagement;
using UnityEngine;
using VContainer;

namespace Gameplay
{
    public class EcsRunner : MonoBehaviour
    {
        private GameContext _context;
        private ITimeService _timeService;
        private GameplayFeature _gameplayFeature;
        private IInputService _inputService;

        [Inject]
        private void Construct
        (
            GameContext context,
            ITimeService timeService,
            IInputService inputService
        )
        {
            _inputService = inputService;
            _timeService = timeService;
            _context = context;
        }
        
        private void Awake()
        {
            _gameplayFeature = new GameplayFeature(_context, _timeService, _inputService);
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