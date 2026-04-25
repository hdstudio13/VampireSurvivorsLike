using Architecture.CameraManagement;
using Architecture.Input;
using Debug;
using Entitas;
using UnityEngine;

namespace Gameplay.Features.Turrets.Systems
{
    public class SetPlayerTurretRotationSystem : IExecuteSystem
    {
        private readonly ICameraService _camera;
        private readonly IGroup<GameEntity> _playerTurrets;

        public SetPlayerTurretRotationSystem
        (
            GameContext context,
            ICameraService camera
        )
        {
            _camera = camera;
            _playerTurrets = context.GetGroup(GameMatcher.AllOf
            (
                GameMatcher.Player,
                GameMatcher.Turret,
                GameMatcher.RotationSpeed,
                GameMatcher.WorldRotation,
                GameMatcher.Transform
            ));
        }
        
        public void Execute()
        {
            foreach (var turret in _playerTurrets)
            {
                var direction = _camera.GetDirectionToMouse(turret.Transform.position);
                var currentYaw = turret.WorldRotation.eulerAngles.y;
                var targetYaw = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                var yawDelta = Mathf.DeltaAngle(currentYaw, targetYaw);
                var deltaRotation = Quaternion.Euler(0f, yawDelta, 0f);
                turret.ReplaceDeltaRotation(deltaRotation);
            }
        }
    }
}