using UnityEngine;

[CreateAssetMenu(fileName = "New UsebleItem Object", menuName = "Inventory System/UsebleItem")]
public class UsableItem : Item {

    public bool IsConsumable;
   /* public virtual void Use(Character character)
    {

    }*/
    public virtual void Use(InventoryManager character)
    {

    }
}
