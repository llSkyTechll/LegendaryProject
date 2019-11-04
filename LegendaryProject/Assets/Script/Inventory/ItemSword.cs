using UnityEngine;

public class ItemSword : MonoBehaviour {
    [SerializeField] Item item;
    [SerializeField] Inventory inventory;
    [SerializeField] KeyCode itemPickupKeyCode = KeyCode.F;

    private bool isInRange;
    private bool isEmpty;

    private void OnValidate()
    {
        if (inventory == null)
        {
            inventory = FindObjectOfType<Inventory>();
        }
    }
    private void Update()
    {
        if (isInRange && Input.GetKeyDown(itemPickupKeyCode))
        {
            if (!isEmpty)
            {
                inventory.AddItem(item);
                Destroy(gameObject);
                isEmpty = true;
            }
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        CheckCollision(other.gameObject, true);

    }
    private void OnTriggerExit(Collider other)
    {
        CheckCollision(other.gameObject, false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckCollision(collision.gameObject, true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        CheckCollision(collision.gameObject, false);
    }
    private void CheckCollision(GameObject gameObject, bool state)
    {
        if (gameObject.CompareTag("Player"))
        {
            isInRange = state;
        }
    }

}
