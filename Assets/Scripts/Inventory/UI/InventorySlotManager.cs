using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

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
        item.SetParentBack();
        SwapItems(item);
    }

    // Swap is bad, but no better ideas
    public void SwapItems(ItemActions _item)
    {
        InventorySlotManager itemSlotManager = _item.GetComponentInParent<InventorySlotManager>();

        InventorySlot slot1 = _assignedInventoryItem;
        InventorySlot slot2 = itemSlotManager.AssignedInventorySlot;

        InventorySystem invSys = GetComponentInParent<StaticInventoryDisplay>().InventorySystem;
        InventorySystem invSys1 = itemSlotManager.GetComponentInParent<StaticInventoryDisplay>().InventorySystem;

        invSys.GetSlotIndex(_assignedInventoryItem, out int slot1Index);
        invSys1.GetSlotIndex(itemSlotManager.AssignedInventorySlot, out int slot2Index);

        invSys.SetSlotByIndex(slot2, slot1Index);
        invSys1.SetSlotByIndex(slot1, slot2Index);
    }

    #endregion

}
