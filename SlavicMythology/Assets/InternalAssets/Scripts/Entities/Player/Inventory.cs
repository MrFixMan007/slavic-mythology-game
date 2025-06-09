using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    List<Item> items = new List<Item>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void addItem(Item newItem) 
    {
        items.Add(newItem);
        //newItem.Use();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
