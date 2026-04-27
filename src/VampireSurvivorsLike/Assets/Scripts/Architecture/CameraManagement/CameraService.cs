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

        public bool IsOnScreen(Vector3 targetPos, float allowedDeviation = 0f)
        {
            Vector3 viewportPoint = _camera.WorldToViewportPoint(targetPos);
            return viewportPoint.x + allowedDeviation >= 0 && viewportPoint.x - allowedDeviation <= 1 && viewportPoint.y + allowedDeviation >= 0 && viewportPoint.y - allowedDeviation <= 1;
        }
    }
}