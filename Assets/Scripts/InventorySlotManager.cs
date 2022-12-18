using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotManager : MonoBehaviour, IDropHandler {
    public string slotType;

    public void OnDrop(PointerEventData eventData) {
        GameObject dropped = eventData.pointerDrag;
        InventoryItemManager inventoryItemManager = dropped.GetComponent<InventoryItemManager>();
        if (transform.childCount == 0) {
            inventoryItemManager.parentAfterDrag = transform;
        } else {
            //InventorySlot slot = inventoryItemManager.inventoryManager.GetSlotByTransform(transform);
            //ItemObject itemObject = inventoryItemManager.inventoryManager.inventory.database.GetItem[slot.item.Id];


            Transform slot2 = transform;
            transform.GetChild(0).SetParent(inventoryItemManager.parentAfterDrag);
            inventoryItemManager.parentAfterDrag = slot2;
        }
    }

    //private bool EqualsType(ItemObject item) {
    //    if (slotType == "" ||  )
    //        return true;
    //    else
    //        return false;
    //}
}
