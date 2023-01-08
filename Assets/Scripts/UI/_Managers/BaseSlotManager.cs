using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSlotManager : MonoBehaviour
{

    #region Fields

    [SerializeField]
    protected InventorySlot _assignedInventoryItem;

    #endregion

    #region Properties

    public InventorySlot AssignedInventorySlot => _assignedInventoryItem;

    #endregion

    #region Methods

    protected virtual void Awake()
    {
        _assignedInventoryItem?.SetEmptySlot();
        ClearSlot();
    }

    public virtual void Init(InventorySlot slot)
    {
        UpdateSlot(slot);
    }

    public virtual void UpdateSlot(InventorySlot slot)
    {
        _assignedInventoryItem = slot;
        if (slot.ID != -1)
            GetComponentInChildren<InventoryItemManager>().UpdateItem(slot);
        else
            ClearSlot();
    }

    public virtual void UpdateSlot()
    {
        if (_assignedInventoryItem != null)
            UpdateSlot(_assignedInventoryItem);
    }

    public virtual void ClearSlot()
    {
        _assignedInventoryItem?.SetEmptySlot();
        GetComponentInChildren<InventoryItemManager>().ClearItem();
    }

    #endregion

}
