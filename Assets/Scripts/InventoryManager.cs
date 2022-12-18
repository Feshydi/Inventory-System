using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {
    public GameObject inventoryPrefab;
    public InventoryObject inventory;
    public bool isDragging;

    Dictionary<GameObject, InventorySlot> itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
    Dictionary<Transform, GameObject> itemsTransform = new Dictionary<Transform, GameObject>();

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
        itemsTransform.Add(child, obj);
        return;
    }

    public void ClearSlots() {
        itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
        itemsTransform = new Dictionary<Transform, GameObject>();
        foreach (Transform _transform in transform) {
            foreach (Transform child in _transform) {
                Destroy(child.gameObject);
            }
        }
    }

    private void SetSlotSprite(GameObject key, InventorySlot value) {
        if (value.ID >= 0) {
//            key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.getItem[value.item.Id].uiDisplay;
            key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
            key.GetComponentInChildren<TextMeshProUGUI>().text = value.amount == 1 ? "" : value.amount.ToString("n0");
        } else {
            key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
            key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
            key.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
    }

    public void SwapItems(Transform obj1Transform, Transform obj2Transform) {
        InventorySlot slot1 = GetSlotByTransform(obj1Transform);
        InventorySlot slot2 = GetSlotByTransform(obj2Transform);
        inventory.SwapItems(slot1, slot2);
        CreateSlots();
    }

    public InventorySlot GetSlotByTransform(Transform _transform) {
        return itemsDisplayed[itemsTransform[_transform]];
    }
}
