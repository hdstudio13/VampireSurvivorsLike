
using Entitas;

namespace Gameplay.Features.Effects
{
    [Game] public class EffectComponent : IComponent { }
    [Game] public class Owner : IComponent { public uint Value; }
    [Game] public class Target : IComponent { public uint Value; }
    [Game] public class Damage : IComponent { public float Value; }
}