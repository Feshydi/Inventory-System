using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class MouseItem : MonoBehaviour
{

    #region Fields

    [SerializeField]
    private Image _itemSprite;

    [SerializeField]
    private TextMeshProUGUI _itemCount;

    [SerializeField]
    private InventorySlot _slot;

    #endregion

    #region Properties

    public Image ItemSprite
    {
        get { return _itemSprite; }
        set { _itemSprite = value; }
    }

    public TextMeshProUGUI ItemCount
    {
        get { return _itemCount; }
        set { _itemCount = value; }
    }

    public InventorySlot Slot
    {
        get { return _slot; }
        set { _slot = value; }
    }

    #endregion

    #region Methods

    private void Awake()
    {
        DisableMouse();
        _itemSprite.raycastTarget = false;
    }

    private void Update()
    {
        transform.position = Mouse.current.position.ReadValue();
    }

    public void SetMouseItem(bool isHalf, InventorySlot slot)
    {
        _itemSprite.sprite = GameManager.Instance.Database.GetItem[slot.ID].Icon;
        _itemSprite.color = Color.white;
        _slot.SetSlot(slot.ID, slot.StackSize / (isHalf ? 2 : 1));

        _itemCount.text = _slot.StackSize == 1 ? "" : _slot.StackSize.ToString();
    }

    public void DisableMouse()
    {
        _itemSprite.sprite = null;
        _itemSprite.color = Color.clear;
        _itemCount.text = "";
        _slot = new InventorySlot();
    }

    #endregion

}
