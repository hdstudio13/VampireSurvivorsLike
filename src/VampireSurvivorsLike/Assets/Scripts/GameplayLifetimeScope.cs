using System;
using Architecture;
using AssetManagement;
using Entitas;
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
        
        // ECS GameContext
        builder.RegisterInstance(Contexts.sharedInstance.game);
        
        // system factory
        builder.Register<SystemFactory>(Lifetime.Singleton).As<ISystemFactory>();
    }
    
    private WindowFactory CreateWindowFactory(IObjectResolver resolver)
    {
        return new WindowFactory(resolver.Resolve<IAssetProvider>(), windowsRoot, Constants.WINDOWS_RESOURCE_PATH);
    }
}