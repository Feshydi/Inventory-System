using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ItemAmount
{
    public ItemObject ItemObject;
    [Range(1, 64)]
    public int Amount;
}

[CreateAssetMenu]
public class CraftingRecipe : ScriptableObject
{

    #region Fields

    [SerializeField]
    private List<ItemAmount> _materials;

    [SerializeField]
    private List<ItemAmount> _results;

    [SerializeField]
    private float _craftingTime;

    #endregion

    #region Properties

    public List<ItemAmount> Materials => _materials;

    public List<ItemAmount> Results => _results;

    #endregion

    #region Methods

    // check inventory for materials
    public bool CanCraft(PlayerController player)
    {
        InventoryHolder[] inventoryHolders = player.GetComponents<InventoryHolder>();

        foreach (var inventory in inventoryHolders)
        {

        }

        return false;
    }

    // put results in inventory
    public void Craft(PlayerController player)
    {

    }

    #endregion

}
