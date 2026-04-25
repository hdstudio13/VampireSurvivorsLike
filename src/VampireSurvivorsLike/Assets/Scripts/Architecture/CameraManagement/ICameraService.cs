using UnityEngine;

namespace Architecture.CameraManagement
{
    public interface ICameraService
    {
        Vector3 GetDirectionToMouse(Vector3 targetPos);
    }
}