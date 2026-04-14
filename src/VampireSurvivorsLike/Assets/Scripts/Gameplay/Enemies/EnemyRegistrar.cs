using System;
using Common;
using UnityEngine;

namespace Gameplay.Enemies
{
    public class EnemyRegistrar : MonoBehaviour
    {
        [SerializeField] private float speed = 2;

        private void Awake()
        {
            Contexts.sharedInstance.game.CreateEntity()
                .AddWorldPosition(transform.position)
                .AddSpeed(speed)
                .AddTransform(transform)
                .With(x => x.isEnemy = true);
        }
    }
}