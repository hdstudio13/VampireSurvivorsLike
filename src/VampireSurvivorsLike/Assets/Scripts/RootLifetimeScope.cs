using AssetManagement;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using WindowsSystem;

public class RootLifetimeScope : LifetimeScope
{
    [SerializeField] private Transform windowsRoot;
    
    protected override void Configure(IContainerBuilder builder)
    {
        // asset management
        builder.Register<AddressableAssetMediator>(Lifetime.Singleton);
        builder.Register<AddressableAssetProvider>(Lifetime.Scoped).As<IAssetProvider>();
        
        // windows system
        builder.Register<IWindowFactory>(CreateWindowFactory, Lifetime.Singleton).Keyed(WindowFactoryType.Persistent);
    }
    
    private WindowFactory CreateWindowFactory(IObjectResolver resolver)
    {
        return new WindowFactory(resolver.Resolve<IAssetProvider>(), windowsRoot, Constants.WINDOWS_RESOURCE_PATH);
    }
}