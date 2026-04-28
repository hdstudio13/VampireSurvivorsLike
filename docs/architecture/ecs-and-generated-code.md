# ECS And Generated Code

## ECS Model

- Gameplay uses Entitas-style ECS.
- Active contexts configured by Jenny: `Game`, `Input`, `Meta`.
- Runtime gameplay systems primarily use `GameContext`.
- Systems are grouped into feature classes under `Assets/Scripts/Gameplay/Features`.
- `SystemFactory` creates systems through VContainer so systems can use constructor injection.

## Component Source

- Components are declared in first-party `*Components.cs` files with `[Game]`.
- Examples:
  - movement components in `Gameplay/Features/Movement/MovementComponents.cs`
  - attack components in `Gameplay/Features/Attacking/AttackComponents.cs`
  - view components in `Gameplay/Features/View/ViewComponents.cs`

## Generated Output

- Generated API appears under `Assets/Scripts/Generated`.
- Do not edit generated files directly.
- Regenerate Entitas code after changing component declarations.
- Generated code is tracked in Git.

