using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class BaseItemActions : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    #region Fields

    [Header("Autosettings")]
    [SerializeField]
    protected PlayerControls _inputActions;

    [SerializeField]
    protected InventoryDescription _description;

    #endregion

    #region Properties

    public InventoryDescription Description
    {
        get => _description;
        set => _description = value;
    }

    #endregion

    #region Methods

    protected virtual void Awake()
    {
        _inputActions = new PlayerControls();
        _description = GameObject.Find("Inventory Description").GetComponent<InventoryDescription>();
    }

    protected virtual void OnEnable() { }

    protected virtual void OnDisable() { }

    public void OnPointerEnter(PointerEventData eventData)
    {
        var id = GetComponent<BaseSlotManager>().AssignedInventorySlot.ID;
        if (id != -1)
        {
            ItemObject itemObject = GameManager.Instance.Database.GetItem[id];
            _description.ShowDescription(itemObject.Title, itemObject.ToString(), itemObject.Description, itemObject.Icon, true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _description.ShowDescription("", false);
    }

    #endregion

}
