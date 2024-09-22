using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float FollowSpeed = 2f;
    public float y_offset = 1f;
    public Transform target;

    void Update()
    {
        Vector3 newpos = new Vector3(target.position.x,target.position.y + y_offset,-10f);
        transform.position = Vector3.Slerp(transform.position,newpos,FollowSpeed*Time.deltaTime);
    }
}
