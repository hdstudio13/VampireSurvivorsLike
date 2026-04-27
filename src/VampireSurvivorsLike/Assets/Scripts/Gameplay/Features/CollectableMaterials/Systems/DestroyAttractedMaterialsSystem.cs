using System.Collections.Generic;
using Entitas;
using Gameplay.Features.EntityDestroy;
using UnityEngine;

namespace Gameplay.Features.CollectableMaterials.Systems
{
    public class DestroyAttractedMaterialsSystem : ICleanupSystem
    {
        private readonly IGroup<GameEntity> _materials;
        private readonly IGroup<GameEntity> _players;
        private readonly List<GameEntity> _buffer = new(32);

        public DestroyAttractedMaterialsSystem(GameContext context)
        {
            _materials = context.GetGroup(GameMatcher
                .AllOf(GameMatcher.Material, GameMatcher.WorldPosition, GameMatcher.MoveSpeed, GameMatcher.Alive));
            _players = context.GetGroup(GameMatcher
                .AllOf(GameMatcher.Player, GameMatcher.WorldPosition, GameMatcher.MoveSpeed).NoneOf(GameMatcher.Turret));
        }
        
        public void Cleanup()
        {
            foreach (var player in _players)
            {
                foreach (var material in _materials.GetEntities(_buffer))
                {
                    if (Vector3.Distance(material.WorldPosition, player.WorldPosition) < 0.5f)
                    {
                        material.SetDead();
                    }
                }
            }
        }
    }
}