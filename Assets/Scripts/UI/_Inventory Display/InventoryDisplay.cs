using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public abstract class InventoryDisplay : MonoBehaviour
{

    #region Fields

    [Header("Auto settings")]
    [SerializeField]
    protected InventorySystem _inventorySystem;

    [SerializeField]
    protected Dictionary<InventorySlotManager, InventorySlot> _slotDictionary;

    [Header("Customizable settings")]
    [SerializeField]
    private InventorySlotManager _slotPrefab;

    #endregion

    #region Properties

    public InventorySystem InventorySystem => _inventorySystem;

    public Dictionary<InventorySlotManager, InventorySlot> SlotDictionary => _slotDictionary;

    #endregion

    #region Methods

    protected virtual void AssignSlots(InventorySystem inventoryToDisplay)
    {
        _slotDictionary = new Dictionary<InventorySlotManager, InventorySlot>();

        for (int i = 0; i < inventoryToDisplay.InventorySize; i++)
        {
            var slot = Instantiate(_slotPrefab, transform);
            _slotDictionary.Add(slot, inventoryToDisplay.InventorySlots[i]);
            slot.Init(inventoryToDisplay.InventorySlots[i]);
        }
    }

    protected virtual void UpdateSlot(InventorySlot updatedSlot)
    {
        foreach (var slot in _slotDictionary)
        {
            if (slot.Value.Equals(updatedSlot))
            {
                slot.Key.UpdateSlot(updatedSlot);
            }
        }
    }

    protected void ClearSlots()
    {
        foreach (Transform _transform in transform)
        {
            Destroy(_transform.gameObject);
        }

        if (_slotDictionary != null)
            _slotDictionary.Clear();
    }

    #endregion

}
