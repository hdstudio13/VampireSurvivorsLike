using System.Collections.Generic;
using Architecture.EntityViews;
using Entitas;

namespace Gameplay.Features.View.Systems
{
    public class ViewCleanUpSystem : ICleanupSystem
    {
        private readonly IGameEntityViewFactory _factory;
        private IGroup<GameEntity> _visible;
        private List<GameEntity> _buffer = new(32);

        public ViewCleanUpSystem(GameContext context, IGameEntityViewFactory factory)
        {
            _factory = factory;
            _visible = context.GetGroup(GameMatcher.AllOf
                (
                    GameMatcher.View,
                    GameMatcher.Destroying)
            );
        }
        
        public void Cleanup()
        {
            foreach (var visibleEntity in _visible.GetEntities(_buffer))
            {
                _factory.Recycle(visibleEntity.View);
                visibleEntity.RemoveView();
            }
        }
    }
}