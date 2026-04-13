using Entitas;
using UnityEngine;

namespace Gameplay.Movement
{
    [Game] public class MoveDirection : IComponent { public Vector3 Value; }
    [Game] public class Speed : IComponent { public float Value; }
}