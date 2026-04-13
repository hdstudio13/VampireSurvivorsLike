using System;
using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerRegistrar : MonoBehaviour
    {
        [SerializeField] private float speed;
        
        private GameEntity _entity;

        private void Awake()
        {
            _entity = Contexts.sharedInstance.game.CreateEntity()
                .AddWorldPosition(transform.position)
                .AddSpeed(speed)
                .AddTransform(transform);
            _entity.isPlayer = true;
        }
    }
}