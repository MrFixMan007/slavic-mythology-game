using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(LootBag))]
[RequireComponent(typeof(Animator))]
public class Chest : MonoBehaviour
{
    private bool looted = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !looted)
        {
            looted = true;
            GetComponent<Animator>().SetTrigger("OnOpen");
            GetComponent<LootBag>().InstantiateLoot(transform.position);
            Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + 2f);
        }
    }
}
