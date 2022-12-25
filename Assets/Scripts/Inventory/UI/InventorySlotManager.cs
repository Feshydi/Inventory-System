using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[System.Serializable]
public class InventorySlotManager : MonoBehaviour, IDropHandler
{

    #region Fields

    [SerializeField]
    private string _slotType;

    [SerializeField]
    private InventorySlot _assignedInventoryItem;

    #endregion

    #region Properties

    public string SlotType
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
        //  _slotType = GetComponentInChildren<InventoryItemManager>().AssignedInventorySlot.ToString();
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
        ItemActions item = dropped.GetComponent<ItemActions>();


        SetSlot(item.ActionWithShift);
    }

    public void SetSlot(bool shifted)
    {
        MouseItem mouse = GetComponentInParent<InventoryController>().MouseItem;
        InventorySystem invSys = GetComponentInParent<InventoryDisplay>().InventorySystem;

        // Is slot empty?
        if (_assignedInventoryItem.ID == -1)
        {
            invSys.ReplaceSlot(_assignedInventoryItem, new InventorySlot(mouse.Slot.ID, mouse.Slot.StackSize));
        }
        else
        {
            // Are both slot items are same?
            if (mouse.Slot.ID == _assignedInventoryItem.ID)
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
                    return;
                }
            }
        }
        mouse.DisableMouse();
    }

    #endregion

}
