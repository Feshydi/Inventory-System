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
    private PlayerControls _inputActions;

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

    private void Awake()
    {
        _inputActions = new PlayerControls();
    }

    private void OnEnable()
    {
        _inputActions.Inventory.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Inventory.Disable();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        InventorySlot selectedSlot = GetComponentInParent<InventorySlotManager>().AssignedInventorySlot;

        SetMouseActive(_inputActions.Inventory.Split.IsPressed(), selectedSlot);
    }

    public void OnDrag(PointerEventData eventData) { }

    public void OnEndDrag(PointerEventData eventData)
    {
        // if mouse not empty place from it back
        GetComponentInParent<InventorySlotManager>().SetSlot(_actionWithShift);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_inputActions.Inventory.Remove.IsPressed())
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

        InventoryController.Instance.MouseItem.SetMouseItem(isHalf, slot);
        GetComponentInParent<InventoryDisplay>().InventorySystem.RemoveFromInventory(slot, isHalf);
    }

    #endregion

}
