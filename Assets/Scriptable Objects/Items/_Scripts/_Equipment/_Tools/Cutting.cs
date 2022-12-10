using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System/Items/Equipment/Tools/Cutting")]
public class Cutting : ToolObject
{
    private void Awake()
    {
        itemType = ItemType.Equipment;
        equipmentType = EquipmentType.Tool;
        toolType = ToolType.Cutting;
        rarityType = RarityType.Common;
    }
}
