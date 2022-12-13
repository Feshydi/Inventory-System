using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {
    public GameObject inventoryPrefab;
    public InventoryObject inventory;
    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();
    Dictionary<Transform, int> itemsTransform = new Dictionary<Transform, int>();

    private void Start() {
        CreateSlots();
    }

    private void LateUpdate() {
        UpdateSlots();
    }

    private void CreateSlots() {
        for (int i = 0; i < inventory.Container.Items.Count; i++) {
            var slot = inventory.Container.Items[i];
            InstantiateNewSlot(slot, i);
        }
    }

    private void UpdateSlots() {
        for (int i = 0; i < inventory.Container.Items.Count; i++) {
            var slot = inventory.Container.Items[i];
            if (itemsDisplayed.ContainsKey(slot))
                itemsDisplayed[slot].GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");
            else
                InstantiateNewSlot(slot, i);
        }
    }

    public void ClearSlots() {
        foreach (Transform transformChild in transform) {
            foreach (Transform child in transformChild) {
                Destroy(child.gameObject);
            }
        }
    }

    public void SwapDisplayedItems(Transform transform1, Transform transform2) {

        int i1 = itemsTransform[transform1];
        int i2 = itemsTransform[transform2];
        InventorySlot inventorySlot = inventory.Container.Items[i1];
        inventory.Container.Items[i1] = inventory.Container.Items[i2];
        inventory.Container.Items[i2] = inventorySlot;

        ClearSlots();
        itemsDisplayed = new Dictionary<InventorySlot, GameObject>();
    }

    private void InstantiateNewSlot(InventorySlot _slot, int index) {
        foreach (Transform transformChild in transform) {
            if (transformChild.childCount == 0) {
                Transform child = transform.GetChild(transformChild.GetSiblingIndex());
                var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, child);
                obj.transform.GetChild(0).GetComponent<Image>().sprite = inventory.database.GetItem[_slot.item.Id].uiDisplay;
                obj.GetComponentInChildren<TextMeshProUGUI>().text = _slot.amount.ToString("n0");
                itemsDisplayed.Add(_slot, obj);
                if (itemsTransform.ContainsKey(child)) {
                    itemsTransform[child] = index;
                } else {
                    itemsTransform.Add(child, index);
                }
                return;
            }
        }
    }
}
