using System.Collections.Generic;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace Gameplay.Features.Common
{
    [Game] public class Id : IComponent { [PrimaryEntityIndex] public uint Value; }
    [Game] public class WorldPosition : IComponent { public Vector3 Value; }
    [Game] public class TransformComponent : IComponent { public Transform Value; }
    [Game] public class Colliders : IComponent { public List<Collider> Value; }
    [Game] public class TargetEntities : IComponent { public List<GameEntity> Value; }
}