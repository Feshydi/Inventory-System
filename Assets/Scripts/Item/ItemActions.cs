using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ItemActions : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{

    #region Fields

    [SerializeField]
    private Image _image;

    [SerializeField]
    private bool _actionWithShift;

    #endregion

    #region Properties

    public Image Image
    {
        get { return _image; }
    }

    public bool ActionWithShift
    {
        get { return _actionWithShift; }
    }

    #endregion

    #region Methods

    public void OnBeginDrag(PointerEventData eventData)
    {
        InventorySlot selectedSlot = GetComponentInParent<InventorySlotManager>().AssignedInventorySlot;

        SetMouseActive(Keyboard.current.shiftKey.isPressed, selectedSlot);
    }

    public void OnDrag(PointerEventData eventData) { }

    public void OnEndDrag(PointerEventData eventData)
    {
        // if not shifting, setting slot from mouse
        if (!_actionWithShift)
            GetComponentInParent<InventorySlotManager>().SetSlot(_actionWithShift);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Keyboard.current.bKey.IsPressed())
        {
            InventorySystem invSys = GetComponentInParent<InventoryDisplay>().InventorySystem;
            invSys.RemoveFromInventory(GetComponentInParent<InventorySlotManager>().AssignedInventorySlot, false);
        }
    }

    public void SetMouseActive(bool isHalf, InventorySlot slot)
    {
        if (slot.StackSize == 1)
            isHalf = false;

        _actionWithShift = isHalf;

        GetComponentInParent<InventoryController>().MouseItem.SetMouseItem(isHalf, slot);
        GetComponentInParent<InventoryDisplay>().InventorySystem.RemoveFromInventory(slot, isHalf);
    }

    #endregion

}
