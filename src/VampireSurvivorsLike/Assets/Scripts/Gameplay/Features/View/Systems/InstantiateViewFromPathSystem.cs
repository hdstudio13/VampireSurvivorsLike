using System.Collections.Generic;
using Architecture.EntityViews;
using AssetManagement;
using Entitas;
using UnityEngine;

namespace Gameplay.Features.View.Systems
{
    public class InstantiateViewFromPathSystem : IExecuteSystem
    {
        private readonly IGameEntityViewFactory _factory;
        private IGroup<GameEntity> _entities;
        private List<GameEntity> _buffer = new(32);

        public InstantiateViewFromPathSystem
        (
            GameContext context,
            IGameEntityViewFactory factory
        )
        {
            _factory = factory;
            _entities = context.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.ViewPath
                ).NoneOf
                (
                    GameMatcher.View,
                    GameMatcher.Destroying
                ));
        }
        
        public void Execute()
        {
            foreach (var entity in _entities.GetEntities(_buffer))
            {
                var view = _factory.Create(entity.ViewPath);
                view.SetEntity(entity);
                entity.ReplaceView(view);
            }
        }
    }
}