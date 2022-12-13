using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {
    public GameObject inventoryPrefab;
    public InventoryObject inventory;
    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();

    private void Start() {
        CreateDisplay();
    }

    private void Update() {
        UpdateDisplay();
    }

    private void CreateDisplay() {
        foreach (var slot in inventory.Container.Items) {
            InstantiateNewSlot(slot);
        }
    }

    private void UpdateDisplay() {
        foreach (var slot in inventory.Container.Items) {
            if (itemsDisplayed.ContainsKey(slot))
                itemsDisplayed[slot].GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");
            else
                InstantiateNewSlot(slot);
        }
    }

    public void ClearDisplay() {
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
    }

    private void InstantiateNewSlot(InventorySlot _slot) {
        var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
        obj.transform.GetChild(0).GetComponent<Image>().sprite = inventory.database.GetItem[_slot.item.Id].uiDisplay;
        obj.GetComponentInChildren<TextMeshProUGUI>().text = _slot.amount.ToString("n0");
        itemsDisplayed.Add(_slot, obj);
    }
}
