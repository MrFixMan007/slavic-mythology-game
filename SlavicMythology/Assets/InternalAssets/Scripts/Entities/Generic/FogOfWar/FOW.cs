using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
public class NewBehaviourScript : MonoBehaviour
{
    private bool entered;
    private Animator _anm;

    private void OnValidate()
    {
        _anm ??= GetComponent<Animator>();
    }

    private void Awake()
    {
        _anm ??= GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !entered)
        {
            entered = true;
            _anm.SetTrigger("Entrance");
            Destroy(gameObject, _anm.GetCurrentAnimatorStateInfo(0).length + 0.1f);
        }
    }
}
