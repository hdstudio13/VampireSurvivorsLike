# Input

## System

- Input uses Unity Input System generated code from `Assets/PlayerInput.inputactions`.
- `InputService` owns and enables the generated `PlayerInput` wrapper.

## Exposed Inputs

- `TurnAxis`
- `GasAxis`
- `Aim`
- `IsAttacking`

## Observed Bindings

- `W` for gas.
- `A/D` for turn.
- Mouse position for aim.
- Left mouse, Enter, gamepad, and touch bindings for attack.

## Task Guidance

- Change bindings in `Assets/PlayerInput.inputactions`, not the generated C# wrapper.
- After modifying input actions, let Unity regenerate `Assets/Scripts/Gameplay/Input/PlayerInput.cs`.

