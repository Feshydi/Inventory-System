using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public InventoryObject inventory;
    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();

    private void Start()
    {
        CreateDisplay();
    }

    private void Update()
    {
        UpdateDisplay();
    }

    private void CreateDisplay()
    {
        foreach (var slot in inventory.Container)
        {
            InstantiateNewSlot(slot);
        }
    }

    private void UpdateDisplay()
    {
        if (inventory.Container.Count == 0)
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }

        foreach (var slot in inventory.Container)
        {
            if (itemsDisplayed.ContainsKey(slot))
            {
                itemsDisplayed[slot].GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");
            }
            else
            {
                InstantiateNewSlot(slot);
            }
        }
    }

    private void InstantiateNewSlot(InventorySlot _slot)
    {
        var obj = Instantiate(_slot.item.prefab, Vector3.zero, Quaternion.identity, transform);
        obj.GetComponentInChildren<TextMeshProUGUI>().text = _slot.amount.ToString("n0");
        itemsDisplayed.Add(_slot, obj);
    }
}
