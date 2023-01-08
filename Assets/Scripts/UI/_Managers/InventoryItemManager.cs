using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryItemManager : MonoBehaviour
{

    #region Fields

    [SerializeField]
    private Image _itemSprite;

    [SerializeField]
    private TextMeshProUGUI _itemCount;

    #endregion

    #region Properties

    public Image ItemSprite
    {
        get { return _itemSprite; }
    }

    public TextMeshProUGUI ItemCount
    {
        get { return _itemCount; }
    }

    #endregion

    #region Methods

    public void UpdateItem(InventorySlot slot)
    {
        ItemObject item = GameManager.Instance.Database.GetItem[slot.ID];

        SetItem(item.Icon, slot.StackSize);
    }

    public void SetItem(Sprite sprite, int amount)
    {
        _itemSprite.sprite = sprite;
        _itemSprite.color = Color.white;
        _itemCount.text = amount > 1 ? amount.ToString() : "";
    }

    public void ClearItem()
    {
        _itemSprite.sprite = null;
        _itemSprite.color = Color.clear;
        _itemCount.text = "";
    }

    #endregion

}
