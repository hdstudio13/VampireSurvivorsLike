using Entitas;

namespace Gameplay.Features.Health
{
    [Game] public class Health : IComponent { public float Value; }
    [Game] public class CurrentHealth : IComponent { public float Value; }
}