using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System/Items/Quest")]
public class QuestObject : ItemObject
{
    private void Awake()
    {
        itemType = ItemType.Quest;
    }
}
