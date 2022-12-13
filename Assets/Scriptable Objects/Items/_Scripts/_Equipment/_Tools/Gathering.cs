using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System/Items/Equipment/Tools/Gathering")]
public class Gathering : ToolObject
{
    private void Awake()
    {
        type = ItemType.Equipment;
        equipmentType = EquipmentType.Tool;
        toolType = ToolType.Gathering;
        rarityType = RarityType.Common;
    }
}
