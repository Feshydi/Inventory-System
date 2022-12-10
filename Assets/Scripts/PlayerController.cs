using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject inventoryWindow;
    public InventoryObject inventory;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ShowOrHideInventoryWindow();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<Item>();
        if (item)
        {
            inventory.AddItem(item.item, 1);
            Destroy(other.gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
    }

    public void SaveInventory()
    {
        inventory.Save();
    }

    public void LoadInvetory()
    {
        inventory.Load();
    }

    public void ClearInventory()
    {
        inventory.Clear();
    }

    public void ShowOrHideInventoryWindow()
    {
        inventoryWindow.SetActive(!inventoryWindow.activeSelf);
    }
}
