using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ItemActions : BaseItemActions,
    IBeginDragHandler, IDragHandler, IEndDragHandler,
    IPointerClickHandler
{

    #region Fields

    [SerializeField]
    private bool _actionWithShift;

    [SerializeField]
    private ItemMenu _menuPrefab;

    [SerializeField]
    private ItemMenu _menu;

    #endregion

    #region Properties

    public bool ActionWithShift => _actionWithShift;

    #endregion

    #region Methods

    protected override void Awake() => base.Awake();

    private void Update()
    {
        if (_menu != null && GetComponentInParent<CanvasGroup>().alpha == 0)
            ExitMenu();
    }

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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_menu != null)
            ExitMenu();

        var slot = GetComponent<InventorySlotManager>().AssignedInventorySlot;

        if (slot.ID == -1)
            return;

        _menu = Instantiate(_menuPrefab, transform);
        _menu.Init(slot, this, GetComponentInParent<InventoryDisplay>().InventorySystem);
        var menuRect = _menu.GetComponent<RectTransform>();
        menuRect.anchoredPosition = Vector2.zero;
        menuRect.anchorMin = new Vector2(1, 0.5f);
        menuRect.anchorMax = new Vector2(1, 0.5f);
        menuRect.pivot = new Vector2(0, 0.5f);
        _menu.transform.SetParent(GetComponentInParent<UIManager>().transform);
        _menu.transform.SetAsLastSibling();
    }

    public void ExitMenu()
    {
        Destroy(_menu.gameObject);
        _menu = null;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        InventorySlot selectedSlot = GetComponent<InventorySlotManager>().AssignedInventorySlot;

        SetMouseActive(_inputActions.Inventory.Split.IsPressed(), selectedSlot);
    }

    public void OnDrag(PointerEventData eventData) { }

    public void OnEndDrag(PointerEventData eventData)
    {
        // if mouse not empty place it back
        GetComponent<InventorySlotManager>().SetSlot(_actionWithShift);
    }

    public void SetMouseActive(bool isHalf, InventorySlot slot)
    {
        if (slot.StackSize == 1)
            isHalf = false;

        _actionWithShift = isHalf;

        GameManager.Instance.MouseItem.SetMouseItem(isHalf, slot);
        GetComponentInParent<InventoryDisplay>().InventorySystem.RemoveFromInventory(slot, isHalf);
    }

    #endregion

}
