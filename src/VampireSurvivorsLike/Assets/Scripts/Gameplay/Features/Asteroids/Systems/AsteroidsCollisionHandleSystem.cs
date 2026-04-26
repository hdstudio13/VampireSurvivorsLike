using System.Collections.Generic;
using Entitas;
using Gameplay.Features.Common.Factories;
using UnityEngine;

namespace Gameplay.Features.Asteroids.Systems
{
    public class AsteroidsCollisionHandleSystem : IExecuteSystem
    {
        private readonly IHitVFXFactory _vfxFactory;
        private readonly IGroup<GameEntity> _asteroids;
        private readonly List<GameEntity> _buffer = new(32);
        private const float MinVectorSqrMagnitude = 0.0001f;
        private const float Restitution = 1f;

        public AsteroidsCollisionHandleSystem(GameContext context, IHitVFXFactory vfxFactory)
        {
            _vfxFactory = vfxFactory;
            _asteroids = context.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Asteroid,
                    GameMatcher.Alive,
                    GameMatcher.TargetEntities,
                    GameMatcher.WorldPosition,
                    GameMatcher.MoveDirection,
                    GameMatcher.MoveSpeed,
                    GameMatcher.PerformingCollisionCheck));
        }
        
        public void Execute()
        {
            foreach (var asteroid in _asteroids.GetEntities(_buffer))
            {
                foreach (var targetEntity in asteroid.TargetEntities)
                {
                    if (targetEntity == null) // player isn't registered in CollisionRegistry
                        continue;

                    if (targetEntity.isProjectile)
                        continue;

                    if (!targetEntity.hasWorldPosition)
                        continue;

                    if (ShouldSkipPair(asteroid, targetEntity))
                        continue;

                    Vector3 collisionNormal = ResolveCollisionNormal(asteroid, targetEntity);
                    ResolveBounce(asteroid, targetEntity, collisionNormal);
                    ResolvePenetration(asteroid, targetEntity, collisionNormal);

                    asteroid.ReplaceCollisionCooldownTimer(0.5f);

                    Vector3 hitPoint = (asteroid.WorldPosition + targetEntity.WorldPosition) / 2;
                    if (asteroid.hasHitVFX)
                        _vfxFactory.Create(asteroid.HitVFX, hitPoint);
                }
            }
        }

        private static bool ShouldSkipPair(GameEntity asteroid, GameEntity targetEntity)
        {
            // Process asteroid-asteroid pair only once per frame to avoid double impulses.
            return targetEntity.isAsteroid && asteroid.hasId && targetEntity.hasId && asteroid.Id >= targetEntity.Id;
        }

        private static Vector3 ResolveCollisionNormal(GameEntity asteroid, GameEntity targetEntity)
        {
            Vector3 delta = targetEntity.WorldPosition - asteroid.WorldPosition;
            if (delta.sqrMagnitude > MinVectorSqrMagnitude)
                return delta.normalized;

            Vector3 asteroidVelocity = asteroid.MoveDirection * asteroid.MoveSpeed;
            Vector3 targetVelocity = targetEntity.hasMoveDirection
                ? targetEntity.MoveDirection * (targetEntity.hasMoveSpeed ? targetEntity.MoveSpeed : 0f)
                : Vector3.zero;

            Vector3 relativeVelocity = asteroidVelocity - targetVelocity;
            if (relativeVelocity.sqrMagnitude > MinVectorSqrMagnitude)
                return relativeVelocity.normalized;

            return asteroid.MoveDirection.sqrMagnitude > MinVectorSqrMagnitude
                ? asteroid.MoveDirection.normalized
                : Vector3.right;
        }

        private static void ResolveBounce(GameEntity asteroid, GameEntity targetEntity, Vector3 normal)
        {
            Vector3 asteroidVelocity = asteroid.MoveDirection * asteroid.MoveSpeed;
            Vector3 targetVelocity = targetEntity.hasMoveDirection
                ? targetEntity.MoveDirection * (targetEntity.hasMoveSpeed ? targetEntity.MoveSpeed : 0f)
                : Vector3.zero;

            float approachingSpeed = Vector3.Dot(asteroidVelocity - targetVelocity, normal);
            if (approachingSpeed <= 0f)
                return;

            float impulse = (1f + Restitution) * approachingSpeed * 0.5f;

            Vector3 asteroidVelocityAfter = asteroidVelocity - impulse * normal;
            if (asteroidVelocityAfter.sqrMagnitude > MinVectorSqrMagnitude)
                asteroid.ReplaceMoveDirection(asteroidVelocityAfter.normalized);

            if (!targetEntity.hasMoveDirection)
                return;

            Vector3 targetVelocityAfter = targetVelocity + impulse * normal;
            if (targetVelocityAfter.sqrMagnitude > MinVectorSqrMagnitude)
                targetEntity.ReplaceMoveDirection(targetVelocityAfter.normalized);
        }

        private static void ResolvePenetration(GameEntity asteroid, GameEntity targetEntity, Vector3 normal)
        {
            if (!asteroid.hasRadius || !targetEntity.hasRadius)
                return;

            Vector3 delta = targetEntity.WorldPosition - asteroid.WorldPosition;
            float distance = delta.magnitude;
            float penetration = asteroid.Radius + targetEntity.Radius - distance;
            if (penetration <= 0f)
                return;

            if (distance > MinVectorSqrMagnitude)
                normal = delta / distance;

            bool targetCanMove = targetEntity.hasMoveDirection;
            float asteroidShare = targetCanMove ? 0.5f : 1f;
            float targetShare = targetCanMove ? 0.5f : 0f;

            Vector3 correction = normal * penetration;
            asteroid.ReplaceWorldPosition(asteroid.WorldPosition - correction * asteroidShare);

            if (targetShare > 0f)
                targetEntity.ReplaceWorldPosition(targetEntity.WorldPosition + correction * targetShare);
        }
    }
}