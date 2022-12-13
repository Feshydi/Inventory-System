using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System/Items/Equipment/Weapon")]
public class WeaponObject : EquipmentObject
{
    public int damageValue;

    private void Awake()
    {
        type = ItemType.Equipment;
        equipmentType = EquipmentType.Weapon;
        rarityType = RarityType.Common;
    }
}
