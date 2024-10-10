using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
public class NewBehaviourScript : MonoBehaviour
{
    private bool entered;
    [SerializeField] private List<Enemy> _enemies;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !entered)
        {
            entered = true;
            GetComponent<Animator>().SetTrigger("Entrance");
            foreach (var enemy in _enemies)
            {
                enemy.SetAggresive();
            }
            Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + 0.1f);
        }
    }
}
