using UnityEngine;

[CreateAssetMenu(fileName = "New Max Health Effect", menuName = "Items/Effects/Max Health Effect")]
public class MaxHealthEffect : ItemEffect
{
    public int healthIncreaseAmount;

    public override void Apply(GameObject player)
    {
        Health playerHealth = player.GetComponent<Health>();
        if (playerHealth != null)
        {
            playerHealth.IncreaseMaxHealth(healthIncreaseAmount);
        }

        
        if (effectPrefab != null)
        {
            GameObject effectInstance = Instantiate(effectPrefab, player.transform.position, Quaternion.identity);
           
            effectInstance.transform.SetParent(player.transform);
           
            Object.Destroy(effectInstance, 2f); 
        }


    }
}
