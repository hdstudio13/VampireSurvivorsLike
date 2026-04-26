using Entitas;

namespace Gameplay.Features.EntityDestroy
{
    [Game] public class Destroying : IComponent {}
    [Game] public class Alive : IComponent {}
    [Game] public class Destroyed : IComponent {}
    [Game] public class DestroyTimer : IComponent { public float Value; }
}