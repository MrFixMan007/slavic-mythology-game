using UnityEngine;

[RequireComponent(typeof(Collider2D))]
//[RequireComponent(typeof(Animator))]
public class Door : MonoBehaviour
{
    public void open()
    {
        this.gameObject.SetActive(false);
        //this.enabled = false;
        //_anm.SetTrigger("Entrance");
        Debug.Log("door open");
    }
    public void close()
    {
        this.gameObject.SetActive(true);
        //this.enabled = true;
        //_anm.SetTrigger("Entrance");
        Debug.Log("door closed");
    }
}
