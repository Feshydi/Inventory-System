using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class InventoryManager : MonoBehaviour {
    public GameObject inventoryPrefab;
    public InventoryObject inventory;

    Dictionary<GameObject, InventorySlot> itemsDisplayed = new Dictionary<GameObject, InventorySlot>();

    private void Start() {
        CreateSlots();
    }

    private void Update() {
        UpdateSlots();
    }

    private void UpdateSlots() {
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in itemsDisplayed) {
            SetSlotSprite(_slot.Key, _slot.Value);
        }
    }

    public void CreateSlots() {
        ClearSlots();
        int childPos = 0;
        foreach (InventorySlot item in inventory.Container.Items) {
            InstantiateSlot(item, childPos);
            childPos++;
        }
        childPos = 0;
    }

    // Check every slot for empty one
    // Create GameObject slot and put in Dictionary
    private void InstantiateSlot(InventorySlot slot, int _childPos) {
        Transform child = transform.GetChild(transform.GetChild(_childPos).GetSiblingIndex());
        var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, child);
        itemsDisplayed.Add(obj, slot);
        return;
    }

    public void ClearSlots() {
        itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
        foreach (Transform _transform in transform) {
            foreach (Transform child in _transform) {
                Destroy(child.gameObject);
            }
        }
    }

    private void SetSlotSprite(GameObject key, InventorySlot value) {
        if (value.ID >= 0) {
            key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[value.item.Id].uiDisplay;
            key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
            key.GetComponentInChildren<TextMeshProUGUI>().text = value.amount == 1 ? "" : value.amount.ToString("n0");
        } else {
            key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
            key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
            key.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
    }

    public void SwapDisplayedItems(Transform transform1, Transform transform2) {

        //int i1 = itemsTransform[transform1];
        //int i2 = itemsTransform[transform2];
        //InventorySlot inventorySlot = inventory.Container.ItemsArray[i1];
        //inventory.Container.ItemsArray[i1] = inventory.Container.ItemsArray[i2];
        //inventory.Container.ItemsArray[i2] = inventorySlot;

        //ClearSlots();
        //itemsDisplayed = new Dictionary<InventorySlot, GameObject>();
    }
}
