using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
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

    #endregion

    #region Methods

    public void Init(InventorySystem inventoryToDisplay)
    {
        _inventorySystem = inventoryToDisplay;
    }

    public void AssignSlots()
    {
        _inventorySystem.OnInventorySlotChanged += UpdateSlot;

        _slotDictionary = new Dictionary<InventorySlotManager, InventorySlot>();

        for (int i = 0; i < _inventorySystem.InventorySize; i++)
        {
            var slot = Instantiate(_slotPrefab, transform);
            _slotDictionary.Add(slot, _inventorySystem.InventorySlots[i]);
            slot.Init(_inventorySystem.InventorySlots[i]);
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

    protected virtual void ClearSlots()
    {
        RemoveSlots();
        if (_slotDictionary != null)
            _slotDictionary.Clear();
    }

    protected void RemoveSlots()
    {
        foreach (Transform _transform in transform)
        {
            Destroy(_transform.gameObject);
        }
    }

    #endregion

}
