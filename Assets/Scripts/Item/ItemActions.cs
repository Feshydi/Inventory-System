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
    private Transform _parentGrabbedItem;

    [SerializeField]
    private Image _image;

    #endregion

    #region Properties

    public Transform ParentGrabbedItem
    {
        get { return _parentGrabbedItem; }
    }

    public Image Image
    {
        get { return _image; }
    }

    #endregion

    #region Methods

    public void OnBeginDrag(PointerEventData eventData)
    {
        _parentGrabbedItem = transform.parent;
        InventorySlot selectedSlot = GetComponentInParent<InventorySlotManager>().AssignedInventorySlot;

        SetMouseActive(Input.GetKey(KeyCode.LeftShift), selectedSlot);
    }

    public void OnDrag(PointerEventData eventData) { }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponentInParent<CanvasManager>().MouseItem.DisableMouse();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Input.GetKey(KeyCode.B))
        {
            InventorySystem _invSys = GetComponentInParent<StaticInventoryDisplay>().InventorySystem;
            _invSys.RemoveFromInventory(GetComponentInParent<InventorySlotManager>().AssignedInventorySlot, false);
        }
    }

    public void SetParentBack()
    {
        transform.SetParent(_parentGrabbedItem);
    }

    public void SetMouseActive(bool isHalf, InventorySlot slot)
    {
        if (slot.StackSize == 1)
            isHalf = false;
        GetComponentInParent<CanvasManager>().MouseItem.SetMouseItem(isHalf, slot);
        GetComponentInParent<StaticInventoryDisplay>().InventorySystem.RemoveFromInventory(slot, isHalf);
    }

    #endregion

}
