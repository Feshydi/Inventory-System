using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BaseItemActions : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    #region Fields

    [SerializeField]
    protected PlayerControls _inputActions;

    [SerializeField]
    private Image _image;

    #endregion

    #region Properties

    #endregion

    #region Methods

    protected virtual void Awake()
    {
        _inputActions = new PlayerControls();
    }

    protected virtual void OnEnable() { }

    protected virtual void OnDisable() { }

    public void OnPointerEnter(PointerEventData eventData)
    {
        var id = GetComponentInParent<BaseSlotManager>().AssignedInventorySlot.ID;
        if (id != -1)
        {
            ItemObject itemObject = GameManager.Instance.Database.GetItem[id];
            InventoryController.Instance.DescriptionText.text = itemObject.ToString();
            InventoryController.Instance.SetDescriptionTextActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InventoryController.Instance.DescriptionText.text = "";
        InventoryController.Instance.SetDescriptionTextActive(false);
    }

    #endregion

}
