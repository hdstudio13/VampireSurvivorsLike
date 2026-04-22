using UnityEngine;

namespace Architecture.Input
{
    public interface IInputService
    {
        float GasAxis { get; }
        float TurnAxis { get; }
    }
}