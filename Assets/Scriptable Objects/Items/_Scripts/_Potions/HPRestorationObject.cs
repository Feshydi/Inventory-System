using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System/Items/Potion/HPRestoration")]
public class HPRestorationObject : PotionObject
{
    public int restoreHealthValue;

    private void Awake()
    {
        itemType = ItemType.Potion;
        potionType = PotionType.HPRestoration;
    }
}
