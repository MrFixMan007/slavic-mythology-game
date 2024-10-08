﻿using VContainer;
using VContainer.Unity;

public class GameLifeTimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register(_ =>
        {
            var gameLoader = new GameLoaderImpl();
            return gameLoader;
        }, Lifetime.Scoped).AsImplementedInterfaces().AsSelf();

        builder.Register<MainMenuViewModel>(Lifetime.Singleton);
        builder.RegisterComponentInHierarchy<MainMenuView>();
    }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
}