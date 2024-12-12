using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windController : MonoBehaviour
{
    public Material[] materials;
    public float windSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var material in materials)
        {
            material.SetFloat("_windSpeed", windSpeed);
        }
    }
}
