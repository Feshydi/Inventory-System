using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System/Items/Equipment/Armor")]
public class ArmorObject : EquipmentObject
{
    public int defenseValue;

    private void Awake()
    {
        type = ItemType.Equipment;
        equipmentType = EquipmentType.Armor;
        rarityType = RarityType.Common;
    }
}
