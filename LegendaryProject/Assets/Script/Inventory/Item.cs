using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Item Object", menuName = "Inventory System/Item")]
public class Item : ScriptableObject {

    [SerializeField] string id;
    public string ID { get { return id; } }
    public string ItemName;
    public Sprite Icon;

    private void OnValidate()
    {
        string path = AssetDatabase.GetAssetPath(this);
        id = AssetDatabase.AssetPathToGUID(path);
    }
}
