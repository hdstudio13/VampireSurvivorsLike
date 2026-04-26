using System.Collections.Generic;
using Architecture.Identification;
using Entitas;
using Gameplay.Features.EntityDestroy;

namespace Gameplay.Features.View.Systems
{
    public class InstantiateDestroyViewSystem : IExecuteSystem
    {
        private readonly GameContext _context;
        private readonly IIdentifierService _identifier;
        private readonly IGroup<GameEntity> _entities;
        private readonly List<GameEntity> _buffer = new(32);

        public InstantiateDestroyViewSystem(GameContext context, IIdentifierService identifier)
        {
            _context = context;
            _identifier = identifier;
            _entities = context.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Destroying,
                    GameMatcher.DestroyViewPath));
        }
        
        public void Execute()
        {
            foreach (var entity in _entities.GetEntities(_buffer))
            {
                _context.CreateEntity()
                    .AddId(_identifier.Next())
                    .AddViewPath(entity.DestroyViewPath)
                    .AddWorldPosition(entity.WorldPosition)
                    .SetAlive()
                    .AddDestroyTimer(2);
                
                entity.RemoveDestroyViewPath();
            }
        }
    }
}