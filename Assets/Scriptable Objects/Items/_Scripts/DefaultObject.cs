using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System/Items/Default")]
public class DefaultObject : ItemObject
{
    private void Awake()
    {
        itemType = ItemType.Default;
    }
}
