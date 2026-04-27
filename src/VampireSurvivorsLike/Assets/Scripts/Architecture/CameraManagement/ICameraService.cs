using UnityEngine;

namespace Architecture.CameraManagement
{
    public interface ICameraService
    {
        Vector3 GetDirectionToMouse(Vector3 targetPos);
        bool IsOnScreen(Vector3 targetPos, float allowedDeviation = 0);
    }
}