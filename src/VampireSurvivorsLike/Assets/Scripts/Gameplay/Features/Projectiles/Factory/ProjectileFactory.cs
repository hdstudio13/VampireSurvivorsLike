using Architecture.EntityViews;
using Architecture.Identification;
using Common;
using UnityEngine;

namespace Gameplay.Features.Projectiles.Factory
{
    public class ProjectileFactory : IProjectileFactory
    {
        private readonly GameContext _context;
        private readonly IIdentifierService _identifier;

        public ProjectileFactory
        (
            GameContext context,
            IIdentifierService identifier
        )
        {
            _context = context;
            _identifier = identifier;
        }
        
        public GameEntity Create(Vector3 position, Vector3 direction)
        {
            return _context.CreateEntity()
                .AddId(_identifier.Next())
                .AddViewPath("EntityViews/ProjectileView.prefab")
                .AddWorldPosition(position)
                .AddWorldRotation(Quaternion.LookRotation(direction))
                .AddMoveSpeed(20)
                .With(x => x.isProjectile = true);
        }
    }
}