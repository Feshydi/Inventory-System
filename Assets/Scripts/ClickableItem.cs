using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ClickableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{

    #region Fields

    [SerializeField]
    private Transform _parentAfterDrag;

    [SerializeField]
    private Image image;

    #endregion

    #region Properties

    public Transform parentAfterDrag
    {
        get { return _parentAfterDrag; }
        set { _parentAfterDrag = value; }
    }

    #endregion

    #region Methods

    public void OnBeginDrag(PointerEventData eventData)
    {
        _parentAfterDrag = transform.parent;

        transform.SetParent(transform.root);
        transform.SetAsLastSibling();

        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Mouse.current.position.ReadValue();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(_parentAfterDrag);

        image.raycastTarget = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Input.GetKey(KeyCode.B))
        {
            InventorySystem _invSys = GetComponentInParent<StaticInventoryDisplay>().InventorySystem;
            _invSys.RemoveFromInventory(GetComponentInParent<InventorySlotManager>().AssignedInventorySlot);
        }
    }

    #endregion

}
