using Entitas;

namespace Gameplay.Features.Attacking
{
    [Game] public class Attacking : IComponent { }
    [Game] public class AbleToAttack : IComponent { }
    [Game] public class AttackCoolDown : IComponent { public float Value; }
    [Game] public class AttackCoolDownTimer : IComponent { public float Value; }
}