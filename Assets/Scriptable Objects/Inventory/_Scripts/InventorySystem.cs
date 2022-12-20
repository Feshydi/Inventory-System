using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[System.Serializable]
public class InventorySystem
{

    #region Fields

    [SerializeField]
    private List<InventorySlot> _inventorySlots;

    private UnityAction<InventorySlot> _onInventorySlotChanged;

    #endregion

    #region Properties

    public List<InventorySlot> InventorySlots
    {
        get { return _inventorySlots; }
    }

    public int InventorySize
    {
        get { return InventorySlots.Count; }
        set { _inventorySlots = new List<InventorySlot>(value); }
    }

    public UnityAction<InventorySlot> OnInventorySlotChanged
    {
        get { return _onInventorySlotChanged; }
    }

    #endregion

    #region Constructors

    public InventorySystem(int size)
    {
        _inventorySlots = new List<InventorySlot>(size);

        for (int i = 0; i < size; i++)
        {
            InventorySlots.Add(new InventorySlot());
        }
    }

    #endregion

    #region Methods

    public bool AddToInventory(ItemObject itemToAdd, int amountToAdd, out int amountLeft)
    {
        amountLeft = amountToAdd;

        if (ContainsItem(itemToAdd, out List<InventorySlot> inventorySlots))    // putting items in existed slots
        {
            foreach (var slot in inventorySlots)
            {
                if (slot.RoomLeftInStack(amountLeft, out int amountRemaining))
                {
                    slot.AddToStack(amountLeft);
                    _onInventorySlotChanged?.Invoke(slot);
                    return true;
                }
                else
                {
                    amountLeft -= amountRemaining;
                    slot.AddToStack(amountRemaining);
                    _onInventorySlotChanged?.Invoke(slot);
                }
            }
        }

        while (amountLeft > 0)                  // while have items creating new slots 
        {
            if (HasFreeSlot(out InventorySlot freeSlot))
            {
                freeSlot.SetInventorySlotWithItem(itemToAdd, amountLeft, out amountLeft);
                _onInventorySlotChanged?.Invoke(freeSlot);
            }
            else
                return false;
        }

        return true;
    }

    public bool ContainsItem(ItemObject itemToAdd, out List<InventorySlot> inventorySlots)
    {
        inventorySlots = _inventorySlots.Where(slot => slot.ID == itemToAdd.ID).ToList();
        return inventorySlots != null ? true : false;
    }

    public bool HasFreeSlot(out InventorySlot freeSlot)
    {
        freeSlot = InventorySlots.FirstOrDefault(slot => slot.ID == -1);
        return freeSlot != null ? true : false;
    }

    #endregion

}
