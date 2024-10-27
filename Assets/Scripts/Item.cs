using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType { Health, Ammo } 
    public ItemType itemType;                      
    public int value = 1;                          

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
}
