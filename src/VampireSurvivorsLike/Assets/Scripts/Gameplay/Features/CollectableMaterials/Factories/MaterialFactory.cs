using System;
using Architecture.Identification;
using Common;
using Gameplay.Features.EntityDestroy;
using UnityEngine;

namespace Gameplay.Features.CollectableMaterials.Factories
{
    public class MaterialFactory : IMaterialFactory
    {
        private readonly GameContext _context;
        private readonly IIdentifierService _identifier;

        public MaterialFactory(GameContext context, IIdentifierService identifier)
        {
            _context = context;
            _identifier = identifier;
        }

        public GameEntity Create(MaterialType type, Vector3 worldPosition)
        {
            return _context.CreateEntity()
                .AddId(_identifier.Next())
                .AddViewPath(GetAssetPathByType(type))
                .AddWorldPosition(worldPosition)
                .SetAlive()
                .AddMoveSpeed(5)
                .With(x => x.isMaterial = true);
        }

        private string GetAssetPathByType(MaterialType type)
        {
            return type switch
            {
                MaterialType.None => "",
                MaterialType.Silver => "EntityViews/Materials/SilverMaterialView.prefab",
                MaterialType.Gold => "EntityViews/Materials/GoldMaterialView.prefab",
                _ => ""
            };
        }
    }
}