using System;
using AssetManagement;
using TimeManagement;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using WindowsSystem;

public class GameplayLifetimeScope : LifetimeScope
{
    [SerializeField] private Transform windowsRoot;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<IWindowFactory>(CreateWindowFactory, Lifetime.Singleton).Keyed(WindowFactoryType.Default);
        builder.RegisterInstance(Contexts.sharedInstance.game);
    }
    
    private WindowFactory CreateWindowFactory(IObjectResolver resolver)
    {
        return new WindowFactory(resolver.Resolve<IAssetProvider>(), windowsRoot, Constants.WINDOWS_RESOURCE_PATH);
    }
}