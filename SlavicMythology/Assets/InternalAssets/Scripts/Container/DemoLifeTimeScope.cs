using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnRadius = 3f;

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
        builder.RegisterComponentInHierarchy<RoomTrigger2D>();
        builder.Register<IEnemySpawner, EnemySpawner>(Lifetime.Singleton)
               .WithParameter("enemyPrefab", enemyPrefab)
               .WithParameter("spawnRadius", spawnRadius); ;
    }
}