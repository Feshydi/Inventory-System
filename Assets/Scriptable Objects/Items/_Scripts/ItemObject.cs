using UnityEngine;

public enum ItemType {
    Equipment,
    Food,
    Potion,
    Resource,
    Quest,
    Default
}

[System.Serializable]
public class Item {
    public string Name;
    public int Id;
    public int MaxStack;

    public Item(ItemObject item) {
        Name = item.name;
        Id = item.Id;
        MaxStack = item.maxStack;
    }
}

public abstract class ItemObject : ScriptableObject {
    [HideInInspector] public int Id;
    public Sprite uiDisplay;
    public ItemType type;
    [TextArea(15, 20)]
    public string description;
    [Range(1, 99)]
    public int maxStack = 1;
}
