using Entitas;
using UnityEngine;

namespace Gameplay.Features.Movement
{
    [Game] public class MoveVector : IComponent { public Vector3 Value; }
    [Game] public class TargetMoveVector : IComponent { public Vector3 Value; }
    [Game] public class MoveSpeed : IComponent { public float Value; }
    [Game] public class Acceleration : IComponent { public float Value; }
    [Game] public class Deacceleration : IComponent { public float Value; }
}