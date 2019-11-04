using UnityEngine;

public enum EquipmentType
{
    Helmet,
    Armor,
    Weapon,
    Accessory,
}
[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Inventory System/Equipment")]
public class EquipableItem : Item {
    public int StrenghtBonus;
    public int DefenseBonus;
    public int VitalityBonus;
    //public int AgilityBonus;
    //public int ItelligenceBonus;

    //[Space]
    //public float StrenghtPercentBonus;
    //public float DefensePercentBonus;
    //public float VitalityPercentBonus;
    //public float AgilityPercentBonus;
    //public float ItelligencePercentBonus;
    [Space]
    public EquipmentType EquipmentType;

}
