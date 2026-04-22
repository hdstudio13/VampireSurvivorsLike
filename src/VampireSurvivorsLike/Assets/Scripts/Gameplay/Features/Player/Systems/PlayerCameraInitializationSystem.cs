using System.Collections.Generic;
using Entitas;
using Unity.Cinemachine;

namespace Gameplay.Features.Player.Systems
{
    public class PlayerCameraInitializationSystem : ReactiveSystem<GameEntity>
    {
        private readonly CinemachineCamera _camera;

        public PlayerCameraInitializationSystem(GameContext context, CinemachineCamera camera) : base(context)
        {
            _camera = camera;
        }
        
        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.AllOf(GameMatcher.Player, GameMatcher.View).Added());
        }

        protected override bool Filter(GameEntity entity) => entity.hasView && entity.isPlayer;

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var player in entities)
            {
                _camera.Follow = player.View.transform;
                _camera.LookAt = player.View.transform;
            }
        }
    }
}