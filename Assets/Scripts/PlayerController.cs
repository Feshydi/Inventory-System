using UnityEngine;

public class PlayerController : MonoBehaviour {
    public GameObject inventoryWindow;
    public InventoryObject inventory;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.I)) {
            ShowOrHideInventoryWindow();
        }
    }

    private void OnTriggerEnter(Collider other) {
        var item = other.GetComponent<GroundItem>();
        if (item && !inventory.IsFull()) {
            inventory.AddItem(new Item(item.item), 1);
            Destroy(other.gameObject);
        }
    }

    private void OnApplicationQuit() {
        ClearInventory();
    }

    public void SaveInventory() {
        inventory.Save();
    }

    public void LoadInvetory() {
        inventory.Load();
        inventoryWindow.GetComponentInChildren<InventoryManager>().CreateSlots();
    }

    public void ClearInventory() {
        inventory.Clear();
        inventoryWindow.GetComponentInChildren<InventoryManager>().CreateSlots();
    }

    public void ShowOrHideInventoryWindow() {
        inventoryWindow.SetActive(!inventoryWindow.activeSelf);
    }
}
