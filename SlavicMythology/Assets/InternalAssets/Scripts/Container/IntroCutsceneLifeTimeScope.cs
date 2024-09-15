using VContainer;
using VContainer.Unity;

public class IntroCutsceneLifeTimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register(_ =>
        {
            var gameLoader = new GameLoaderImpl();
            return gameLoader;
        }, Lifetime.Scoped).AsImplementedInterfaces().AsSelf();
        
        builder.RegisterComponentInHierarchy<FairyTaleCutscene>();
    }
}