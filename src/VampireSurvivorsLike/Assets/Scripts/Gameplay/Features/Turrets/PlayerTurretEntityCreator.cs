using System;
using Architecture.EntityViews.GameContext;
using Architecture.Identification;
using Common;
using UnityEngine;
using VContainer;

namespace Gameplay.Features.Turrets
{
    public class PlayerTurretEntityCreator : MonoBehaviour
    {
        [SerializeField] private GameEntityView view;
        
        private GameContext _context;
        private IIdentifierService _identifierService;

        [Inject]
        private void Construct
        (
            GameContext context,
            IIdentifierService identifierService
        )
        {
            _identifierService = identifierService;
            _context = context;
        }
        
        private void Start()
        {
            var entity = _context
                .CreateEntity()
                .AddId(_identifierService.Next())
                .AddView(view)
                .AddWorldRotation(Quaternion.identity)
                .AddRotationSpeed(20)
                .With(x => x.isAbleToAttack = true)
                .With(x => x.isTurret = true)
                .With(x => x.isPlayer = true);
            
            view.SetEntity(entity);
        }
    }
}