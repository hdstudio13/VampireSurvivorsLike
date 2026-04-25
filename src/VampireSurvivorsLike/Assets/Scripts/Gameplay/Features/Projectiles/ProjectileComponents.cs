using Entitas;
using UnityEngine;

namespace Gameplay.Features.Projectiles
{
    [Game] public class ProjectileComponent : IComponent { }
    [Game] public class ProjectileAttack : IComponent { }
    [Game] public class ProjectilePivot : IComponent { public Transform Value; }
}