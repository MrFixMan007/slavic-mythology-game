using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnRadius = 3f;
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private RoomTrigger2D roomTriggerPrefab;

    protected override void Configure(IContainerBuilder builder)
    {
        // Регистрация префаба врага
        //builder.RegisterComponentInNewPrefab(enemyPrefab);

        // Регистрация фабрики врагов
        builder.Register<EnemyFactory>(Lifetime.Singleton)
               .As<IEnemyFactory>()
               .WithParameter(enemyPrefab);

        //builder.Register<Player>().AsSelf().AsImplementedInterfaces().WithParameter("health", 100);
        builder.RegisterEntryPoint<GameManager>();

        builder.RegisterComponentInHierarchy<Health>();

        //builder.RegisterComponentInHierarchy<RoomTrigger2D>();

        builder.Register<EnemySpawner>(Lifetime.Singleton)
            .As<IEnemySpawner>()
            .WithParameter("spawnRadius", spawnRadius)
            .WithParameter("obstacleMask", obstacleMask);

        // builder.RegisterComponentInHierarchy<RoomTrigger2D>();
    }

    private void Start()
    {
        var enemySpawner = Container.Resolve<IEnemySpawner>();
        foreach (var roomTrigger in FindObjectsOfType<RoomTrigger2D>())
        {
            roomTrigger.SetEnemySpawner(enemySpawner);
        }
    }

}
