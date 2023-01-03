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

        _itemSprite.sprite = item.Icon;
        _itemSprite.color = Color.white;

        if (slot.StackSize > 1)
            ItemCount.text = slot.StackSize.ToString();
        else
            ItemCount.text = "";
    }

    public void ClearItem()
    {
        _itemSprite.sprite = null;
        _itemSprite.color = Color.clear;
        _itemCount.text = "";
    }

    #endregion

}
