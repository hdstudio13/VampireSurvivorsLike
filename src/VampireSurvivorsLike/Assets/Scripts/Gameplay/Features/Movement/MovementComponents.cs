using Entitas;
using UnityEngine;

namespace Gameplay.Features.Movement
{
    [Game] public class Moving : IComponent { }
    [Game] public class MoveDirection : IComponent { public Vector3 Value; }
    [Game] public class MoveSpeed : IComponent { public float Value; }
    [Game] public class MaxMoveSpeed : IComponent { public float Value; }
    [Game] public class Acceleration : IComponent { public float Value; }
    [Game] public class Deacceleration : IComponent { public float Value; }
}