# Project Layout

## Roots

- Repository root contains collaboration docs, Jenny tooling, and `src/`.
- Unity project root is `src/VampireSurvivorsLike`.
- Unity version is `6000.3.12f1`.
- First-party gameplay code lives in `src/VampireSurvivorsLike/Assets/Scripts`.

## Important Runtime Assets

- `Assets/Scenes/GameplayScene.unity`
- `Assets/Prefabs/RootLifetimeScope.prefab`
- `Assets/Prefabs/GameplayLifetimeScope.prefab`
- `Assets/Prefabs/EntityViews/**`
- `Assets/ScriptableObjects/Configs/GameConfig.asset`
- `Assets/ScriptableObjects/Configs/PlayerConfig.asset`

## Major Areas

- `Assets/Scripts/Architecture` - composition, services, state machine integration, entity view infrastructure.
- `Assets/Scripts/Gameplay` - gameplay feature systems, components, factories, input-generated wrapper, player config/controller.
- `Assets/Scripts/Generated` - Entitas/Jenny generated code. Do not edit directly.
- `Assets/Scripts/HD*` - local utility/framework-style modules for assets, attributes, state machines, debug, and windows.
- `Assets/Entitas`, `Assets/JMO Assets`, `Assets/Plugins` - vendor or third-party-heavy areas.

## Repository Hygiene

- `.gitignore` excludes Unity-generated directories under `src/VampireSurvivorsLike`: `Library`, `Logs`, `Temp`, `UserSettings`.
- Generated Unity solution/project files are ignored, although local `.csproj`/`.sln` files may exist after opening the project.
- No first-party test folders were found in `Assets` during the initial research pass.

