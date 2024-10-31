using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType { Health, Ammo, Coin} 
    public ItemType itemType;                      
    public int value = 1;
    public float spawnWeight;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Item collided with " + other.name);
        if (other.CompareTag("Player"))  
        {
            Debug.Log("Item collected by player");
            Player player = other.GetComponentInChildren<Player>();
            if (player != null)
            {
                player.CollectItem(this); 
                Destroy(gameObject); 
                Debug.Log("Item collected");
            }
        }
    }

    private void Start()
    {
        if (itemType == ItemType.Coin)
        {
            value = Random.Range(10, 25);
        }
    }
}
