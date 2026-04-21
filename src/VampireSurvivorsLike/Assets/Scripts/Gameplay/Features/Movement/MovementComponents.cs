using Entitas;
using UnityEngine;

namespace Gameplay.Features.Movement
{
    [Game] public class MoveVector : IComponent { public Vector2 Value; }
    [Game] public class Speed : IComponent { public float Value; }
}