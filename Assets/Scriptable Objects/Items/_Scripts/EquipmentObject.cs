public enum EquipmentType
{
    Armor,
    Jewelry,
    Weapon,
    Tool,
}

public enum RarityType
{
    Common,
    Rare,
    Legendary
}

public class EquipmentObject : ItemObject
{
    public EquipmentType equipmentType;
    public RarityType rarityType;
    public int durability;
}
