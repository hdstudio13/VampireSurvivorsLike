using Entitas;
using UnityEngine;

namespace Gameplay.Features.Rotation
{
    [Game] public class WorldRotation : IComponent { public Quaternion Value; }
    [Game] public class DeltaRotation : IComponent {public Quaternion Value;}
    [Game] public class RotationSpeed : IComponent { public float Value; }
}