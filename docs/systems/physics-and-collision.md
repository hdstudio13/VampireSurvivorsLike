# Physics And Collision

## Services

- `SphereCollisionCheckSystem` asks `PhysicsService` for `Physics.OverlapSphereNonAlloc` results.
- `CollisionRegistry` maps collider instance IDs to `GameEntity` through view registrars.
- `PhysicsService` stores overlap results in a fixed collider buffer and writes mapped entities into `TargetEntities`.

## Collision Notes

- Some overlap results may be `null` when a collider is not registered.
- Asteroid collision handling explicitly skips null targets.
- Projectile collision handling creates damage effects for all targets, clears `TargetEntities`, and kills the projectile.
- Asteroid collisions skip projectile targets.
- Asteroid-asteroid pairs are processed once per frame by comparing entity IDs.
- Asteroid collisions apply bounce and penetration correction, then optionally spawn hit VFX.

## Task Guidance

- For new collidable views, ensure the prefab has `CollidersRegistrar`.
- For ECS collision checks, entities need `PerformingCollisionCheck`, `Radius`, `LayerMask`, `TargetEntities`, `WorldPosition`, and `Alive`.
- Use `CullingCollision` when checks should be skipped off-screen.

