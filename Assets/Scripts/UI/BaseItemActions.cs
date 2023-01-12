using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class BaseItemActions : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    #region Fields

    [SerializeField]
    protected PlayerControls _inputActions;

    [System.NonSerialized]
    private static UnityAction<string, bool> _onPointedItem;

    #endregion

    #region Properties

    public static UnityAction<string, bool> OnPointedItem
    {
        get => _onPointedItem;
        set => _onPointedItem = value;
    }

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
            OnPointedItem.Invoke(itemObject.ToString(), true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnPointedItem.Invoke("", false);
    }

    #endregion

}
