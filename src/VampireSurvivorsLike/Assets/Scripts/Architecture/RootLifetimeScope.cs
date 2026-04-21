using Architecture.EntityViews;
using Architecture.EntityViews.GameContext;
using Architecture.GameStates;
using Architecture.Identification;
using Architecture.Input;
using Architecture.Systems;
using Architecture.TimeManagement;
using AssetManagement;
using Common;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using WindowsSystem;

namespace Architecture
{
    public class RootLifetimeScope : LifetimeScope
    {
        [SerializeField] private Transform windowsRoot;
    
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<Instantiator>(Lifetime.Scoped).As<IInstantiator>();
        
            // asset management
            builder.Register<AddressableAssetMediator>(Lifetime.Singleton);
            builder.Register<AddressableAssetProvider>(Lifetime.Scoped).As<IAssetProvider>();
        
            // identification service
            builder.Register<IdentifierService>(Lifetime.Singleton).As<IIdentifierService>();
            
            // windows system
            builder.Register<IWindowFactory>(CreateWindowFactory, Lifetime.Singleton).Keyed(WindowFactoryType.Persistent);
        
            // time management
            builder.Register<UnityTimeService>(Lifetime.Singleton).As<ITimeService>();
            
            // game state machine
            builder.RegisterEntryPoint<GameController>();
            
            // contexts
            builder.RegisterInstance(Contexts.sharedInstance.game);
            
            // system factory
            builder.Register<SystemFactory>(Lifetime.Singleton).As<ISystemFactory>();
            
            // game entity view factory
            builder.Register<GameEntityViewFactory>(Lifetime.Singleton).As<IGameEntityViewFactory>();
            
            // input
            builder.RegisterEntryPoint<InputService>();
        }
    
        private WindowFactory CreateWindowFactory(IObjectResolver resolver)
        {
            return new WindowFactory(resolver.Resolve<IAssetProvider>(), windowsRoot, Constants.WINDOWS_RESOURCE_PATH);
        }
    }
}