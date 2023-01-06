using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public class InventorySlotManager : MonoBehaviour, IDropHandler
{

    #region Fields

    [SerializeField]
    private ItemObject _slotType;

    [SerializeField]
    private InventorySlot _assignedInventoryItem;

    #endregion

    #region Properties

    public ItemObject SlotType
    {
        get { return _slotType; }
    }

    public InventorySlot AssignedInventorySlot
    {
        get { return _assignedInventoryItem; }
        set { _assignedInventoryItem = value; }
    }

    #endregion

    #region Methods

    private void Awake()
    {
        _assignedInventoryItem?.SetEmptySlot();
        ClearSlot();
    }

    public void Init(InventorySlot slot)
    {
        UpdateSlot(slot);
    }

    public void UpdateSlot(InventorySlot slot)
    {
        _assignedInventoryItem = slot;
        if (slot.ID != -1)
            GetComponentInChildren<InventoryItemManager>().UpdateItem(slot);
        else
            ClearSlot();
    }

    public void UpdateSlot()
    {
        if (_assignedInventoryItem != null)
            UpdateSlot(_assignedInventoryItem);
    }

    public void ClearSlot()
    {
        _assignedInventoryItem?.SetEmptySlot();
        GetComponentInChildren<InventoryItemManager>().ClearItem();
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        var item = dropped.GetComponent<ItemActions>();
        var mouseSlot = InventoryController.Instance.MouseItem.Slot;
        var fromSlotType = item.GetComponentInParent<InventorySlotManager>()._slotType;

        // if mouse slot empty return
        if (mouseSlot.ID == -1)
            return;

        // if slots have no type
        if (_slotType == null && fromSlotType == null)
        {
            SetSlot(item.ActionWithShift);
            return;
        }
        else
        // if slots have same type
        if (_slotType != null)
        {
            if (_slotType.GetType().Name == mouseSlot.ToString())
            {
                SetSlot(item.ActionWithShift);
                return;
            }
        }
        else
        // if slot-in have no type and no item
        if (_slotType == null && _assignedInventoryItem.ID == -1)
        {
            SetSlot(item.ActionWithShift);
            return;
        }
        else
        // if slot-in have no type, but item have same type as slot-out
        if (fromSlotType != null)
        {
            if (_slotType == null && fromSlotType.GetType().Name == _assignedInventoryItem.ToString())
            {
                SetSlot(item.ActionWithShift);
                return;
            }
        }
    }

    public void SetSlot(bool shifted)
    {
        var mouse = InventoryController.Instance.MouseItem;
        var invSys = GetComponentInParent<InventoryDisplay>().InventorySystem;

        // if no items in mouse
        if (mouse.Slot.ID == -1)
            return;

        // Is slot empty?
        if (_assignedInventoryItem.ID == -1)
        {
            invSys.ReplaceSlot(_assignedInventoryItem, new InventorySlot(mouse.Slot.ID, mouse.Slot.StackSize));
        }
        else
        {
            // Are both slot items are same and their maxstacksize not zero?
            if (mouse.Slot.ID == _assignedInventoryItem.ID && GameManager.Instance.Database.GetItem[mouse.Slot.ID].MaxStackSize > 1)
            {
                invSys.FillSlot(_assignedInventoryItem, mouse.Slot.StackSize, out int itemCountLeftInMouse);
                mouse.Slot.StackSize = itemCountLeftInMouse;

                // Does mouse have items?
                if (itemCountLeftInMouse != 0)
                    invSys.AddToInventory(GameManager.Instance.Database.GetItem[mouse.Slot.ID], mouse.Slot.StackSize, out int amountLeft);
                //                                 *** in ideal situation there should be 0 amountLeft... ***
                // #to do: add auto drop item with amountLeft
            }
            else
            {
                // if taken half of item, then put it in first founded slot(s)
                if (shifted)
                {
                    invSys.AddToInventory(GameManager.Instance.Database.GetItem[mouse.Slot.ID], mouse.Slot.StackSize, out int amountLeft);
                }
                else
                {
                    InventorySlot tempSlot = new InventorySlot(_assignedInventoryItem.ID, _assignedInventoryItem.StackSize);
                    invSys.ReplaceSlot(_assignedInventoryItem, new InventorySlot(mouse.Slot.ID, mouse.Slot.StackSize));
                    mouse.Slot = tempSlot;
                    mouse.UpdateItemDisplay();
                    return;
                }
            }
        }

        mouse.DisableMouse();
    }

    #endregion

}
