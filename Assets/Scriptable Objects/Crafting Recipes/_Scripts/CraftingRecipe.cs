using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
    public bool CanCraft(PlayerController player)
    {
        // add check for enough place in inventories

        // create dictionary with needed items ID
        Dictionary<int, int> items = new Dictionary<int, int>();
        foreach (var item in _materials.Select(material => material.ItemObject).ToList())
        {
            items.Add(item.ID, 0);
        }

        InventoryHolder[] inventoryHolders = player.GetComponents<InventoryHolder>();
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
            Debug.Log("need " + material.Amount + " of " + material.ItemObject.Title);
            Debug.Log("have " + items[material.ItemObject.ID]);

            if (items[material.ItemObject.ID] < material.Amount)
                return false;
        }

        return true;
    }

    // put results in inventory
    public void Craft(PlayerController player)
    {
        InventoryHolder[] inventoryHolders = player.GetComponents<InventoryHolder>();

        // iterate materials and remove them from static inventories
        foreach (var item in _materials)
        {
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
            var itemObject = item.ItemObject;
            var itemAmount = item.Amount;

            foreach (var inventory in inventoryHolders)
            {
                inventory.InventorySystem.AddToInventory(itemObject, itemAmount, out itemAmount);
            }
        }
    }

    #endregion

}
