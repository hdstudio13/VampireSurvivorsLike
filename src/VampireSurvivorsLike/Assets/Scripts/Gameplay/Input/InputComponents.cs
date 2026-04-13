using Entitas;
using UnityEngine;

namespace Gameplay.Input
{
    [Game] public class InputComponent : IComponent {}
    [Game] public class AxisInput : IComponent { public Vector2 Value; }
}