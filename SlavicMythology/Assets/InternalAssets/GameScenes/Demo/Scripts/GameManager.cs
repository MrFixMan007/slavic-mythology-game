using UnityEngine;
using VContainer;

public class GameManager : MonoBehaviour
{
    private IEnemyFactory enemyFactory;

    [Inject]
    public void Construct(IEnemyFactory enemyFactory)
    {
        this.enemyFactory = enemyFactory;
    }

    void Start()
    {
        // Пример создания врага
        enemyFactory.CreateEnemy(new Vector3(0, 0, 0));
    }
}