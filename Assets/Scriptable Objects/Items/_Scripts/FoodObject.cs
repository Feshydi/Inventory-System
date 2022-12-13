using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System/Items/Food")]
public class FoodObject : ItemObject
{
    public int saturationValue;

    private void Awake()
    {
        type = ItemType.Food;
    }
}
