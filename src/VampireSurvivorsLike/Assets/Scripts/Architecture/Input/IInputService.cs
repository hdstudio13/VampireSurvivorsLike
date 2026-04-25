using UnityEngine;

namespace Architecture.Input
{
    public interface IInputService
    {
        float GasAxis { get; }
        float TurnAxis { get; }
        Vector2 Aim { get; }
        bool IsAttacking { get; }
    }
}