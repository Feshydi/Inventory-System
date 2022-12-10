using UnityEngine;

public enum ItemType
{
    Equipment,
    Food,
    Potion,
    Resource,
    Quest,
    Default
}

public abstract class ItemObject : ScriptableObject
{
    public GameObject prefab;
    public ItemType itemType;
    [TextArea(15, 20)]
    public string description;
}
