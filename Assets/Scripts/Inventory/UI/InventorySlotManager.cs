using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class InventorySlotManager : MonoBehaviour, IDropHandler
{

    #region Fields

    [SerializeField]
    private string _slotType;

    [SerializeField]
    private InventorySlot _assignedInventoryItem;

    [SerializeField]
    private UnityAction<InventorySlot, Image> _onItemSelected;

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

    public UnityAction<InventorySlot, Image> OnItemSelected
    {
        get { return _onItemSelected; }
        set { _onItemSelected = value; }
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

        SetSlot(item);
    }

    public void SetSlot(ItemActions item)
    {
        MouseItem mouse = GetComponentInParent<CanvasManager>().MouseItem;
        InventorySystem invSys = GetComponentInParent<StaticInventoryDisplay>().InventorySystem;

        if (_assignedInventoryItem.ID == -1)
        {
            invSys.GetSlotIndex(_assignedInventoryItem, out int index);
            invSys.SetSlotByIndex(new InventorySlot(GameManager.Instance.Database.GetItem[mouse.Slot.ID], mouse.Slot.StackSize), index);
        }
        else
        {

        }
    }

    #endregion

}
