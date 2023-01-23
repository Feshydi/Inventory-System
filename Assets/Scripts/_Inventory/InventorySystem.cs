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

    [System.NonSerialized]
    private UnityAction<InventorySlot> _onInventorySlotChanged;

    #endregion

    #region Properties

    public List<InventorySlot> InventorySlots => _inventorySlots;

    public int InventorySize
    {
        get => InventorySlots.Count;
        set => _inventorySlots = new List<InventorySlot>(value);
    }

    public UnityAction<InventorySlot> OnInventorySlotChanged
    {
        get => _onInventorySlotChanged;
        set => _onInventorySlotChanged = value;
    }

    #endregion

    #region Constructors

    public InventorySystem(int size)
    {
        _inventorySlots = new List<InventorySlot>(size);

        for (int i = 0; i < size; i++)
        {
            _inventorySlots.Add(new InventorySlot());
        }
    }

    #endregion

    #region Methods

    public bool AddToInventory(ItemObject itemToAdd, int amountToAdd, out int amountLeft)
    {
        amountLeft = amountToAdd;

        // put items in existed slots
        if (ContainsItem(itemToAdd, out List<InventorySlot> inventorySlots))
        {
            foreach (var slot in inventorySlots)
            {
                FillSlot(slot, amountLeft, out amountLeft);

                if (amountLeft <= 0)
                    return true;
            }
        }

        // while have items creat new slots 
        while (amountLeft > 0)
        {
            if (!HasFreeSlot(out InventorySlot freeSlot))
                return false;

            freeSlot.SetSlot(itemToAdd, amountLeft, out amountLeft);
            _onInventorySlotChanged?.Invoke(freeSlot);
        }

        return true;
    }

    public void FillSlot(InventorySlot slot, int amountToAdd, out int amountLeft)
    {
        if (slot.RoomLeftInStack(amountToAdd, out int amountRemaining))
            slot.AddToStack(amountToAdd);
        else
            slot.AddToStack(amountRemaining);

        amountLeft = amountToAdd <= amountRemaining ? 0 : amountToAdd - amountRemaining;

        _onInventorySlotChanged?.Invoke(slot);
    }

    public void RemoveFromInventory(InventorySlot slotToRemove, bool isHalf)
    {
        foreach (var _slot in _inventorySlots)
        {
            if (_slot.Equals(slotToRemove))
            {
                if (isHalf && _slot.StackSize > 1)
                    _slot.SetSlot(_slot.ID, _slot.StackSize / 2 + _slot.StackSize % 2);
                else
                    _slot.SetEmptySlot();

                _onInventorySlotChanged?.Invoke(_slot);
                return;
            }
        }
    }

    public void ReplaceSlot(InventorySlot slotToReplace, InventorySlot slotToSet)
    {
        foreach (var _slot in _inventorySlots)
        {
            if (_slot.Equals(slotToReplace))
            {
                _slot.SetSlot(slotToSet.ID, slotToSet.StackSize);
                _onInventorySlotChanged?.Invoke(_slot);
                return;
            }
        }
    }

    public bool ContainsItem(ItemObject searchItem, out List<InventorySlot> inventorySlots)
    {
        inventorySlots = _inventorySlots.Where(slot => slot.ID == searchItem.ID).ToList();
        return inventorySlots != null ? true : false;
    }

    public bool HasFreeSlot(out InventorySlot freeSlot)
    {
        freeSlot = InventorySlots.FirstOrDefault(slot => slot.ID == -1);
        return freeSlot != null ? true : false;
    }

    public bool HasFreeSlotCount(out int slotCount)
    {
        slotCount = InventorySlots.FindAll(slot => slot.ID == -1).Count;
        return slotCount != 0 ? true : false;
    }

    public void RemoveItemAmount(ItemObject itemToRemove, int amount, out int amountLeft)
    {
        amountLeft = amount;

        if (ContainsItem(itemToRemove, out List<InventorySlot> inventorySlots))
        {
            foreach (var slot in inventorySlots)
            {
                while (slot.StackSize > 0 && amountLeft > 0)
                {
                    slot.removeFromStack(1);
                    amountLeft -= 1;
                }

                _onInventorySlotChanged?.Invoke(slot);
            }
        }
    }

    #endregion

}
