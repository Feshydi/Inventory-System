using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System/Items/Equipment/Tools/Gathering")]
public class Gathering : ToolObject
{
    private void Awake()
    {
        itemType = ItemType.Equipment;
        equipmentType = EquipmentType.Tool;
        toolType = ToolType.Gathering;
        rarityType = RarityType.Common;
    }
}
