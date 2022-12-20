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
        _assignedInventoryItem = slot;
        UpdateSlot(slot);
    }

    public void UpdateSlot(InventorySlot slot)
    {
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
        ClickableItem clickable = dropped.GetComponent<ClickableItem>();
        clickable.parentAfterDrag = transform;

    }

    #endregion



    //public void OnDrop(PointerEventData eventData)
    //{
    //    GameObject dropped = eventData.pointerDrag;
    //    InventoryItemManager inventoryItemManager = dropped.GetComponent<InventoryItemManager>();
    //    if (transform.childCount == 0)
    //    {
    //        inventoryItemManager.parentAfterDrag = transform;
    //    }
    //    else
    //    {
    //        //InventorySlot slot = inventoryItemManager.inventoryManager.GetSlotByTransform(transform);
    //        //ItemObject itemObject = inventoryItemManager.inventoryManager.inventory.database.GetItem[slot.item.Id];


    //        Transform slot2 = transform;
    //        transform.GetChild(0).SetParent(inventoryItemManager.parentAfterDrag);
    //        inventoryItemManager.parentAfterDrag = slot2;
    //    }
    //}

    //private bool EqualsType(ItemObject item) {
    //    if (slotType == "" ||  )
    //        return true;
    //    else
    //        return false;
    //}
}
