using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Gameplay.Features.Movement.Systems
{
    public class SetUpMoveVectorSystem : ReactiveSystem<GameEntity>
    {
        public SetUpMoveVectorSystem(GameContext context) : base(context)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher
                .AllOf(GameMatcher.TargetMoveVector)
                .NoneOf(GameMatcher.MoveVector)
                .Added()
            );
        }

        protected override bool Filter(GameEntity entity) => entity.hasTargetMoveVector && !entity.hasMoveVector;

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.AddMoveVector(Vector3.zero);
            }
        }
    }
}