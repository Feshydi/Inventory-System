using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemActions : BaseItemActions, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{

    #region Fields

    [SerializeField]
    private bool _actionWithShift;

    #endregion

    #region Properties

    public bool ActionWithShift => _actionWithShift;

    #endregion

    #region Methods

    protected override void Awake() => base.Awake();

    protected override void OnEnable()
    {
        base.OnEnable();
        _inputActions.Inventory.Enable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _inputActions.Inventory.Disable();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        InventorySlot selectedSlot = GetComponent<InventorySlotManager>().AssignedInventorySlot;

        SetMouseActive(_inputActions.Inventory.Split.IsPressed(), selectedSlot);
    }

    public void OnDrag(PointerEventData eventData) { }

    public void OnEndDrag(PointerEventData eventData)
    {
        // if mouse not empty place from it back
        GetComponent<InventorySlotManager>().SetSlot(_actionWithShift);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_inputActions.Inventory.Remove.IsPressed())
        {
            InventorySystem invSys = GetComponentInParent<InventoryDisplay>().InventorySystem;
            invSys.RemoveFromInventory(GetComponent<InventorySlotManager>().AssignedInventorySlot, false);
        }
    }

    public void SetMouseActive(bool isHalf, InventorySlot slot)
    {
        if (slot.StackSize == 1)
            isHalf = false;

        _actionWithShift = isHalf;

        InventoryController.Instance.MouseItem.SetMouseItem(isHalf, slot);
        GetComponentInParent<InventoryDisplay>().InventorySystem.RemoveFromInventory(slot, isHalf);
    }

    #endregion

}
