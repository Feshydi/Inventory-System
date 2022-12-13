using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotManager : MonoBehaviour, IDropHandler {
    public void OnDrop(PointerEventData eventData) {
        GameObject dropped = eventData.pointerDrag;
        InventoryItemManager inventoryItemManager = dropped.GetComponent<InventoryItemManager>();
        if (transform.childCount == 0) {
            inventoryItemManager.parentAfterDrag = transform;
        } else {
            Transform slot2 = transform;
            transform.GetChild(0).transform.SetParent(inventoryItemManager.parentAfterDrag);
            inventoryItemManager.parentAfterDrag = slot2;
        }
    }
}
