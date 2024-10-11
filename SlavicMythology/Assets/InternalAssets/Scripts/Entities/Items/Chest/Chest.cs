using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(LootBag))]
[RequireComponent(typeof(Animator))]
public class Chest : MonoBehaviour
{
    private bool looted = false;
    private Animator _anm;
    private LootBag _lbg;

    private void OnValidate()
    {
        _anm ??= GetComponent<Animator>();
        _lbg ??= GetComponent<LootBag>();
    }

    private void Awake()
    {
        _anm ??= GetComponent<Animator>();
        _lbg ??= GetComponent<LootBag>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !looted)
        {
            looted = true;
            _anm.SetTrigger("OnOpen");
            _lbg.InstantiateLoot(transform.position);
            Destroy(gameObject, _anm.GetCurrentAnimatorStateInfo(0).length + 2f);
        }
    }
}
