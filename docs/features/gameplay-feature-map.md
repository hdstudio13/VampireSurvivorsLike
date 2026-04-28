# Gameplay Feature Map

## Feature Order

`GameplayFeature` currently adds features in this order:

1. Collision checks
2. Player
3. Turrets
4. Projectiles
5. Asteroids
6. Rotation
7. View
8. Animation
9. Attack
10. Effects
11. Health
12. Collectable materials
13. Movement
14. Destroy

## Main Gameplay Behavior

- `PlayerInitializationSystem` creates the player entity from `PlayerConfig`.
- Player movement uses `Gas` input to toggle movement and moves along the player's current `WorldRotation`.
- Player rotation aims through `CameraService.GetDirectionToMouse`.
- `PlayerTurretEntityCreator` creates a player-owned turret entity from the turret view prefab.
- Turret attack input sets `Attacking` on player turrets.
- `ProcessProjectileAttackSystem` creates projectiles when an attacker has `Attacking`, `ProjectileAttack`, and `ProjectilePivot`, and no active attack cooldown timer.
- `ProjectileFactory` creates moving projectile entities with damage, lifetime, view path, and destroy-view path.
- `AsteroidSpawnSystem` maintains `Constants.TARGET_ASTEROIDS_COUNT` around the players.
- `AsteroidFactory` creates asteroids with random view variant, movement, hit VFX, and optional material type.
- `ProjectileCollisionHandleSystem` creates damage effects for projectile targets and kills the projectile.
- `ProcessDamageEffectsSystem` subtracts damage from targets with `CurrentHealth`.
- `SpawnMaterialsSystem` drops material pickups when destroyed asteroids have a material type.
- `AttractNearbyMaterialsToPlayer` pulls material pickups toward nearby players.
- Destroy systems process timers, mark dead entities as destroying/destroyed, and clean up entities/views.

## Feature Task Guidance

- For a gameplay behavior task, start here, then open only the matching feature folder under `Assets/Scripts/Gameplay/Features`.
- For a new ECS component, edit the source component file, regenerate Jenny output, then inspect generated diffs.
- For behavior touching view prefabs, also read `systems/views-and-assets.md`.
- For behavior touching collision or targeting, also read `systems/physics-and-collision.md`.

