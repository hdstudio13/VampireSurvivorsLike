using Architecture.CameraManagement;
using Architecture.EntityPhysics;
using Architecture.EntityViews;
using Architecture.Input;
using Architecture.Systems;
using AssetManagement;
using Gameplay.Features.Asteroids.Factory;
using Gameplay.Features.Common.Factories;
using Gameplay.Features.Effects.Factory;
using Gameplay.Features.Projectiles.Factory;
using Unity.Cinemachine;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using WindowsSystem;

namespace Architecture
{
    public class GameplayLifetimeScope : LifetimeScope
    {
        [SerializeField] private Transform windowsRoot;
        [Space]
        [SerializeField] private UnityEngine.Camera mainCamera;
        [SerializeField] private CinemachineCamera cinemachineCamera;
        [SerializeField] private CinemachineBrain cinemachineBrain;

        protected override void Configure(IContainerBuilder builder)
        {
            // windows
            builder.Register<IWindowFactory>(CreateWindowFactory, Lifetime.Singleton).Keyed(WindowFactoryType.Default);
            
            // contexts
            builder.RegisterInstance(Contexts.sharedInstance.game);
            
            // system factory
            builder.Register<SystemFactory>(Lifetime.Singleton).As<ISystemFactory>();
            
            // game entity view factory
            builder.Register<GameEntityViewFactory>(Lifetime.Singleton).As<IGameEntityViewFactory>();
            
            // projectile factory
            builder.Register<ProjectileFactory>(Lifetime.Singleton).As<IProjectileFactory>();
            
            // camera
            builder.RegisterInstance(cinemachineCamera);
            builder.RegisterInstance(cinemachineBrain);
            builder.RegisterInstance(mainCamera);
            builder.Register<CameraService>(Lifetime.Singleton).As<ICameraService>();
            
            // physics
            builder.Register<CollisionRegistry>(Lifetime.Singleton).As<ICollisionRegistry>();
            builder.Register<PhysicsService>(Lifetime.Singleton).As<IPhysicsService>();
            
            // effects factory
            builder.Register<EffectsFactory>(Lifetime.Singleton).As<IEffectsFactory>();
            
            // asteroid factory
            builder.Register<AsteroidFactory>(Lifetime.Singleton).As<IAsteroidFactory>();
            
            // hit vfx factory
            builder.Register<HitVFXFactory>(Lifetime.Singleton).As<IHitVFXFactory>();
        }
    
        private WindowFactory CreateWindowFactory(IObjectResolver resolver)
        {
            return new WindowFactory(resolver.Resolve<IAssetProvider>(), windowsRoot, Constants.WINDOWS_RESOURCE_PATH);
        }
    }
}