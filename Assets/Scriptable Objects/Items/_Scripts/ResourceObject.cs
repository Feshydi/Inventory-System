using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System/Items/Resource")]
public class ResourceObject : ItemObject
{
    public int durability;

    private void Awake()
    {
        itemType = ItemType.Resource;
    }
}
