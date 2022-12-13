using UnityEngine;

public class PlayerController : MonoBehaviour {
    public GameObject inventoryWindow;
    public InventoryObject inventory;

    private void Start() {
        LoadInvetory();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.I)) {
            ShowOrHideInventoryWindow();
        }
    }

    private void OnTriggerEnter(Collider other) {
        var item = other.GetComponent<GroundItem>();
        if (item) {
            if (!inventory.Container.IsFull())
                inventory.AddItem(new Item(item.item), 1);
            else if (!inventory.AddItemToStack(new Item(item.item), 1))
                return;
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
        inventoryWindow.GetComponentInChildren<InventoryManager>().ClearSlots();
        inventory.Load();
    }

    public void ClearInventory() {
        inventoryWindow.GetComponentInChildren<InventoryManager>().ClearSlots();
        inventory.Clear();
    }

    public void ShowOrHideInventoryWindow() {
        inventoryWindow.SetActive(!inventoryWindow.activeSelf);
    }
}
