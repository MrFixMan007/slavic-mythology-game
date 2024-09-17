using VContainer;
using VContainer.Unity;

public class GameInstaller : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<Health>(Lifetime.Scoped);
        builder.Register<PlayerController>(Lifetime.Transient);
    }
}