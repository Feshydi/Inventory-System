using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StaticInventoryDisplay : InventoryDisplay
{

    #region Fields

    [SerializeField]
    private InventoryHolder _inventoryHolder;

    [SerializeField]
    private InventorySlotManager[] _slots;

    #endregion

    #region Properties

    public InventoryHolder InventoryHolder
    {
        get { return _inventoryHolder; }
    }

    public InventorySlotManager[] Slots
    {
        get { return _slots; }
    }

    #endregion

    #region Methods

    protected override void Start()
    {
        base.Start();

        if (_inventoryHolder != null)
        {
            _inventorySystem = _inventoryHolder.InventorySystem;
            _inventorySystem.OnInventorySlotChanged += UpdateSlot;
            _inventorySystem.OnInventorySlotChanged += AssignSlot;
        }
        else
            Debug.LogWarning($"No inventory assigned to {gameObject}");

        AssignSlot(_inventorySystem);
    }

    public override void AssignSlot(InventorySystem inventoryToDisplay)
    {
        _slotDictionary = new Dictionary<InventorySlotManager, InventorySlot>();

        if (_slots.Length != inventoryToDisplay.InventorySize)
            Debug.Log($"Inventory slots out of sync on {gameObject}");

        for (int i = 0; i < inventoryToDisplay.InventorySize; i++)
        {
            _slotDictionary.Add(_slots[i], inventoryToDisplay.InventorySlots[i]);
            _slots[i].Init(inventoryToDisplay.InventorySlots[i]);
        }
    }

    #endregion

}
