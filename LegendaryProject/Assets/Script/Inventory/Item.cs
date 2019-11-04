using UnityEngine;

[CreateAssetMenu(fileName = "New Item Object", menuName = "Inventory System/Item")]
public class Item : ScriptableObject {
    public string ItemName;
    public Sprite Icon;
}
