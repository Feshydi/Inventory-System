using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System/Items/Equipment/Tools/Mining")]
public class Mining : ToolObject
{
    private void Awake()
    {
        type = ItemType.Equipment;
        equipmentType = EquipmentType.Tool;
        toolType = ToolType.Mining;
        rarityType = RarityType.Common;
    }
}
