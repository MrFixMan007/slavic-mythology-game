using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            //Destroy(gameObject);
        }
    }
}
