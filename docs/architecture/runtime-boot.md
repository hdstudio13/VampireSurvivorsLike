# Runtime Boot

## Composition Roots

- `RootLifetimeScope` is the root VContainer composition scope.
- `GameplayLifetimeScope` is created from a prefab during gameplay initialization.

## RootLifetimeScope Registers

- `Instantiator` as `IInstantiator`
- Addressables-backed asset/config services
- `IdentifierService`
- persistent window factory
- `UnityTimeService`
- `GameController` entry point
- `InputService` entry point

## GameplayLifetimeScope Registers

- default window factory
- shared `GameContext`
- `SystemFactory`
- `GameEntityViewFactory`
- projectile, asteroid, material, hit VFX, and effects factories
- camera and Cinemachine instances
- collision registry and physics service

## State Flow

1. `GameController.Initialize()` creates an `HDStateMachine`.
2. It adds `LaunchState` and `GameplayInitializationState`.
3. `LaunchState` immediately transitions to `GameplayInitializationState`.
4. `GameplayInitializationState` loads `GameConfig`, creates `GameplayLifetimeScope` from prefab, creates/replaces `GameplayState`, then enters gameplay.
5. `GameplayState` owns `GameplayFeature`; every tick it calls `Execute()` then `Cleanup()`.

