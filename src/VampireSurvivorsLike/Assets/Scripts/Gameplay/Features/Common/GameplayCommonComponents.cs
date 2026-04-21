using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace Gameplay.Features.Common
{
    [Game] public class Id : IComponent { [PrimaryEntityIndex] public uint Value; }
    [Game] public class WorldPosition : IComponent { public Vector2 Value; }
    [Game] public class TransformComponent : IComponent { public Transform Value; }
}