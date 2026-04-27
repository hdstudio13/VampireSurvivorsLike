using System.Collections.Generic;
using Architecture.CameraManagement;
using Entitas;
using Gameplay.Features.EntityDestroy;

namespace Gameplay.Features.CollectableMaterials.Systems
{
    public class CullingMaterialsSystem : ICleanupSystem
    {
        private readonly ICameraService _camera;
        private readonly IGroup<GameEntity> _materials;
        private readonly List<GameEntity> _buffer = new(32);

        public CullingMaterialsSystem(GameContext context, ICameraService camera)
        {
            _camera = camera;
            _materials = context.GetGroup(GameMatcher
                .AllOf(GameMatcher.Material, GameMatcher.WorldPosition, GameMatcher.MoveSpeed, GameMatcher.Alive));
        }
        
        public void Cleanup()
        {
            foreach (var material in _materials.GetEntities(_buffer))
            {
                if (!_camera.IsOnScreen(material.WorldPosition))
                {
                    material.SetDead();
                }
            }
        }
    }
}