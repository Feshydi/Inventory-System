using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static UnityEditor.Progress;

[System.Serializable]
public struct ItemAmount
{
    public ItemObject ItemObject;
    [Range(1, 64)]
    public int Amount;

    public ItemAmount(ItemObject itemObject, int amount)
    {
        ItemObject = itemObject;
        Amount = amount;
    }
}

[CreateAssetMenu]
public class CraftingRecipe : ScriptableObject
{

    #region Fields

    [SerializeField]
    private List<ItemAmount> _materials;

    [SerializeField]
    private List<ItemAmount> _results;

    #endregion

    #region Properties

    public List<ItemAmount> Materials => _materials;

    public List<ItemAmount> Results => _results;

    #endregion

    #region Methods

    // check inventory for materials
    public bool CanCraft(PlayerInventoryController player)
    {
        InventoryHolder[] inventoryHolders = player.GetComponents<InventoryHolder>();

        // check for slot in inventories
        // work correct if every result item belongs to different classes           !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        foreach (var result in _results)
        {
            var inventoryHolder = result.ItemObject.GetInventoryType(player);
            if (!inventoryHolder.InventorySystem.HasFreeSlot(out InventorySlot slot))
                return false;
        }

        // create dictionary with needed items ID
        Dictionary<int, int> items = new Dictionary<int, int>();
        foreach (var item in _materials.Select(material => material.ItemObject).ToList())
        {
            items.Add(item.ID, 0);
        }

        // iterate static inventories
        foreach (var inventory in inventoryHolders)
        {
            List<InventorySlot> inventorySlots = inventory.InventorySystem.InventorySlots;
            // get all slots with needed items
            var result = inventorySlots.Where(slot => _materials.Any(material => slot.ID.Equals(material.ItemObject.ID)));
            // add amount of slot items in dictionary
            foreach (var item in result)
            {
                items[item.ID] += item.StackSize;
            }
        }

        // check if enough items counted
        // if one of items less, instantly return false
        foreach (var material in _materials)
        {
            if (items[material.ItemObject.ID] < material.Amount)
                return false;
        }

        return true;
    }

    // put results in inventory
    public void Craft(PlayerInventoryController player)
    {
        // iterate materials and remove them from static inventories
        foreach (var item in _materials)
        {
            var inventoryHolders = player.GetComponents<InventoryHolder>();

            var itemObject = item.ItemObject;
            var itemAmount = item.Amount;

            foreach (var inventory in inventoryHolders)
            {
                if (itemAmount > 0)
                    inventory.InventorySystem.RemoveItemAmount(itemObject, itemAmount, out itemAmount);
            }
        }
        // add results in static inventories
        foreach (var item in _results)
        {
            var inventoryHolder = item.ItemObject.GetInventoryType(player);
            var itemObject = item.ItemObject;
            var itemAmount = item.Amount;

            inventoryHolder.InventorySystem.AddToInventory(itemObject, itemAmount, out itemAmount);
        }
    }

    #endregion

}
