using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Gameplay.Features.EntityCollisions
{
    [Game] public class PerformingCollisionCheck : IComponent { }
    [Game] public class Radius : IComponent { public float Value; }
    [Game] public class LayerMaskComponent : IComponent { public LayerMask Value; }
    [Game] public class CollisionCooldownTimer : IComponent { public float Value; }
}