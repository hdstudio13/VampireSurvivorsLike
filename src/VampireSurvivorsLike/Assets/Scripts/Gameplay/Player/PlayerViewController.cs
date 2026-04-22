using System;
using Architecture.EntityViews.GameContext;
using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerViewController : GameEntityRegistrar
    {
        [SerializeField] private ParticleSystem leftFire;
        [SerializeField] private ParticleSystem rightFire;
        
        private GameEntity _entity;
        private bool _fireOn;

        public override void RegisterComponents(GameEntity entity)
        {
            _entity = entity;
        }

        public override void UnregisterComponents(GameEntity entity)
        {
            _entity = null;
        }

        private void FixedUpdate()
        {
            UpdateFireParticlesState();
        }

        private void UpdateFireParticlesState()
        {
            if (_entity != null)
            {
                if (_entity.hasTargetMoveVector && _entity.TargetMoveVector.magnitude > 0.00001f)
                {
                    if (!_fireOn)
                    {
                        leftFire.Play();
                        rightFire.Play();
                    }

                    _fireOn = true;
                }
                else
                {
                    if (_fireOn)
                    {
                        leftFire.Stop();
                        rightFire.Stop();
                    }
                    
                    _fireOn = false;
                }
            }
        }
    }
}