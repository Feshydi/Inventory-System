using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScreenManager : MonoBehaviour {
    public void ItemSwap(Transform parentAfterDrag, Transform parentBeforeDrag) {
        InventoryManager inventoryManagerObj1 = parentAfterDrag.GetComponentInParent<InventoryManager>();
        InventoryManager inventoryManagerObj2 = parentBeforeDrag.GetComponentInParent<InventoryManager>();

        if (inventoryManagerObj1 == inventoryManagerObj2) {
            inventoryManagerObj1.SwapItems(parentAfterDrag, parentBeforeDrag);
            return;
        }

        OldInventorySlot slot1 = inventoryManagerObj1.GetSlotByTransform(parentAfterDrag);
        OldInventorySlot slot2 = inventoryManagerObj2.GetSlotByTransform(parentBeforeDrag);
        int slot1Index = inventoryManagerObj1.inventory.GetItemIndex(slot1);
        int slot2Index = inventoryManagerObj2.inventory.GetItemIndex(slot2);

        OldInventorySlot tempSlot = inventoryManagerObj1.inventory.Container.Items[slot1Index];
        inventoryManagerObj1.inventory.Container.Items[slot1Index] = inventoryManagerObj2.inventory.Container.Items[slot2Index];
        inventoryManagerObj2.inventory.Container.Items[slot2Index] = tempSlot;

        inventoryManagerObj1.CreateSlots();
        inventoryManagerObj2.CreateSlots();
    }
}
