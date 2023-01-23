using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class ItemMenu : MonoBehaviour
{

    #region Fields

    [SerializeField]
    private InventorySlot _slot;

    [SerializeField]
    private ItemActions _parent;

    [SerializeField]
    private InventorySystem _inventorySystem;

    #endregion

    #region Properties

    public ItemActions Parent => _parent;

    #endregion

    #region Methods

    public void Init(InventorySlot slot, ItemActions parent, InventorySystem inventorySystem)
    {
        _slot = slot;
        _parent = parent;
        _inventorySystem = inventorySystem;
    }

    public void DropItem()
    {
        _inventorySystem.RemoveFromInventory(_slot, false);

        _parent.ExitMenu();
    }

    public void UseItem()
    {

        _parent.ExitMenu();
    }

    #endregion

}
