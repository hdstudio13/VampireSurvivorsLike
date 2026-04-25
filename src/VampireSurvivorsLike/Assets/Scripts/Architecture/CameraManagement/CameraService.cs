using Architecture.Input;
using UnityEngine;

namespace Architecture.CameraManagement
{
    public class CameraService : ICameraService
    {
        private readonly Camera _camera;
        private readonly IInputService _input;

        public CameraService
        (
            Camera camera,
            IInputService input
        )
        {
            _camera = camera;
            _input = input;
        }

        public Vector3 GetDirectionToMouse(Vector3 targetPos)
        {
            Plane plane = new Plane(Vector3.up, targetPos);
            Ray ray = _camera.ScreenPointToRay(_input.Aim);
            plane.Raycast(ray, out float distance);
            Vector3 targetPointOnPlane = ray.GetPoint(distance);
            return (targetPointOnPlane - targetPos).normalized;
        }
    }
}