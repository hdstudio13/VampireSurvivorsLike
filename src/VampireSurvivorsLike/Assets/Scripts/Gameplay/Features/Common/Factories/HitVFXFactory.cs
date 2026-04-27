using System;
using Architecture.CameraManagement;
using Architecture.Identification;
using Gameplay.Features.EntityDestroy;
using UnityEngine;

namespace Gameplay.Features.Common.Factories
{
    public class HitVFXFactory : IHitVFXFactory
    {
        private readonly GameContext _context;
        private readonly IIdentifierService _identifier;
        private readonly ICameraService _cameraService;

        public HitVFXFactory(GameContext context, IIdentifierService identifier, ICameraService cameraService)
        {
            _context = context;
            _identifier = identifier;
            _cameraService = cameraService;
        }
        
        public GameEntity Create(HitVFXType type, Vector3 position, float autoDestroySeconds = 2)
        {
            if (!_cameraService.IsOnScreen(position))
            {
                return null;
            }
            
            return _context.CreateEntity()
                .AddId(_identifier.Next())
                .AddViewPath(GetAssetPathByType(type))
                .AddWorldPosition(position)
                .SetAlive()
                .AddDestroyTimer(autoDestroySeconds);
        }

        private string GetAssetPathByType(HitVFXType type)
        {
            return type switch
            {
                HitVFXType.None => "",
                HitVFXType.Stone => "EntityViews/Hits/StoneHitView.prefab",
                HitVFXType.Metal => "EntityViews/Hits/MetalHitView.prefab",
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}