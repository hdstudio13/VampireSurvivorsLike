# Views And Assets

## Configs

- `ConfigProvider` loads ScriptableObject configs through Addressables using `Configs/{TypeName}.asset`.
- Known configs:
  - `Assets/ScriptableObjects/Configs/GameConfig.asset`
  - `Assets/ScriptableObjects/Configs/PlayerConfig.asset`

## Entity Views

- Entity views are loaded by Addressables string paths such as `EntityViews/ProjectileView.prefab`.
- `GameEntityViewFactory` pools `GameEntityView` instances per asset path.
- Views are injected by VContainer after instantiation.
- Active and pooled views are parented under a generated `[EntityViews]` hierarchy.

## Registrars

View prefabs use registrars to add/remove Unity object references to entities:

- `TransformRegistrar` adds `Transform`.
- `CollidersRegistrar` adds colliders and registers them in `CollisionRegistry`.
- `AnimatorRegistrar` adds `Animator`.

## Common Entity View Paths

- `EntityViews/ProjectileView.prefab`
- `EntityViews/BlueProjectileDestroyView.prefab`
- `EntityViews/ExplosionView.prefab`
- `EntityViews/Asteroids/AsteroidView{1-6}_{None|Silver|Gold}.prefab`
- `EntityViews/Materials/SilverMaterialView.prefab`
- `EntityViews/Materials/GoldMaterialView.prefab`

