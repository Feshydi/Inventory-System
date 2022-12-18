using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.ComponentModel;

public class InventoryItemManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler {
    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public Transform parentBeforeDrag;
    public Image image;

    [HideInInspector] public InventoryManager inventoryManager;
    public GameObject infoWindow;
    private GameObject info;

    private void Start() {
        inventoryManager = transform.GetComponentInParent<InventoryManager>();
    }

    private void Update() {
        image.raycastTarget = inventoryManager.isDragging ? false : true;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        inventoryManager.isDragging = true;
        parentAfterDrag = transform.parent;
        parentBeforeDrag = parentAfterDrag;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData) {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData) {
        transform.SetParent(parentAfterDrag);
        if (parentAfterDrag != parentBeforeDrag)
            GetComponentInParent<PlayerScreenManager>().ItemSwap(parentAfterDrag, parentBeforeDrag);
        this.inventoryManager.isDragging = false;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (!inventoryManager.isDragging) {
            InventorySlot slot = inventoryManager.GetSlotByTransform(transform.parent);
            if (slot.ID >= 0) {
                //ItemObject item = inventoryManager.inventory.database.getItem[slot.item.Id];

                //foreach (TextMeshProUGUI text in infoWindow.GetComponentsInChildren<TextMeshProUGUI>()) {
                //    if (text.name == "Name Text") {
                //        text.text = item.name;
                //    } else if (text.name == "Type Text") {
                //        text.text = item.type.ToString();
                //    } else if (text.name == "Description Text") {
                //        text.text = item.description;
                //    }
                //}
                info = Instantiate(infoWindow, transform.parent.parent.parent.parent.parent);
                info.transform.SetAsLastSibling();
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (info)
            Destroy(info.gameObject);
    }
}
