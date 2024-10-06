using UnityEngine;

public abstract class ItemEffect : ScriptableObject
{
    public GameObject effectPrefab;
    public abstract void Apply(GameObject player);
}
