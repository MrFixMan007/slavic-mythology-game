using UnityEngine;
using VContainer;
using VContainer.Unity;

public class RoomTrigger2D : MonoBehaviour
{
    [Inject] private IEnemySpawner _enemySpawner;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _enemySpawner.SpawnEnemies(other.transform.position);
        }
    }
}