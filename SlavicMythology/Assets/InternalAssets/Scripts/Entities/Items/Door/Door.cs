using UnityEngine;

[RequireComponent(typeof(Collider2D))]
//[RequireComponent(typeof(Animator))]
public class Door : MonoBehaviour
{
    private Collider2D _c2d;

    private void OnValidate()
    {
        _c2d ??= GetComponent<Collider2D>();
    }

    private void Awake()
    {
        _c2d ??= GetComponent<Collider2D>();
    }

    public void open()
    {
        _c2d.enabled = true;
        //_anm.SetTrigger("Entrance");
        Debug.Log("door open");
    }
    public void close() 
    {
        _c2d.enabled = false;
        //_anm.SetTrigger("Entrance");
        Debug.Log("door closed");
    }
}
