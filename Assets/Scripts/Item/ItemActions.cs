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
    private Transform _parentAfterDrag;

    [SerializeField]
    private Image _image;

    #endregion

    #region Properties

    public Transform ParentAfterDrag
    {
        get { return _parentAfterDrag; }
    }

    public Image Image
    {
        get { return _image; }
    }

    #endregion

    #region Methods

    public void OnBeginDrag(PointerEventData eventData)
    {
        _parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();

        _image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Mouse.current.position.ReadValue();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (transform.parent != _parentAfterDrag)
            transform.SetParent(_parentAfterDrag);

        _image.raycastTarget = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Input.GetKey(KeyCode.B))
        {
            InventorySystem _invSys = GetComponentInParent<StaticInventoryDisplay>().InventorySystem;
            _invSys.RemoveFromInventory(GetComponentInParent<InventorySlotManager>().AssignedInventorySlot);
        }
    }

    public void SetParentBack()
    {
        transform.SetParent(_parentAfterDrag);
    }

    #endregion

}
