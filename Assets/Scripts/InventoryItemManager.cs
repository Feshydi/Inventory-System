using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public Transform parentBeforeDrag;
    public Image image;

    public void OnBeginDrag(PointerEventData eventData) {
        parentAfterDrag = transform.parent;
        parentBeforeDrag = parentAfterDrag;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData) {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData) {
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
        InventoryManager InvMan = transform.GetComponentInParent<InventoryManager>();
        InvMan.SwapItems(parentAfterDrag, parentBeforeDrag);
    }
}
