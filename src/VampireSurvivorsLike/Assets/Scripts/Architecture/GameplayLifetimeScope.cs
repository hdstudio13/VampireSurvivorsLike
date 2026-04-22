using Architecture.EntityViews;
using Architecture.Input;
using Architecture.Systems;
using AssetManagement;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using WindowsSystem;

namespace Architecture
{
    public class GameplayLifetimeScope : LifetimeScope
    {
        [SerializeField] private Transform windowsRoot;

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
        }
    
        private WindowFactory CreateWindowFactory(IObjectResolver resolver)
        {
            return new WindowFactory(resolver.Resolve<IAssetProvider>(), windowsRoot, Constants.WINDOWS_RESOURCE_PATH);
        }
    }
}