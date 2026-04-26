using UnityEngine;

namespace Architecture
{
    public static class Extensions
    {
        public static Vector3 ToTopDown(this Vector2 vector)
        {
            return new Vector3(vector.x, 0, vector.y);
        }
    }
}