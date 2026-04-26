using Architecture.Identification;
using Common;
using Gameplay.Features.EntityDestroy;

namespace Gameplay.Features.Effects.Factory
{
    public class EffectsFactory : IEffectsFactory
    {
        private readonly GameContext _context;
        private readonly IIdentifierService _identifierService;

        public EffectsFactory
        (
            GameContext context,
            IIdentifierService identifierService
        )
        {
            _context = context;
            _identifierService = identifierService;
        }
        
        public GameEntity CreateDamage(float damage, uint targetId, uint ownerId)
        {
            return _context.CreateEntity()
                .AddId(_identifierService.Next())
                .AddTarget(targetId)
                .AddOwner(ownerId)
                .AddDamage(damage)
                .SetAlive()
                .With(x => x.isEffect = true);
        }
    }
}