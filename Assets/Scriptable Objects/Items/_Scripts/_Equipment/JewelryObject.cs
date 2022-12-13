using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System/Items/Equipment/Jewelry")]
public class Jewelry : EquipmentObject
{
    public int additionalHealthValue;
    public int additionalDefenseValue;
    public int additionalDamageValue;

    private void Awake()
    {
        type = ItemType.Equipment;
        equipmentType = EquipmentType.Jewelry;
        rarityType = RarityType.Common;
    }
}
