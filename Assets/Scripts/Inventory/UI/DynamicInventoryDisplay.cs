using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DynamicInventoryDisplay : InventoryDisplay
{

    #region Fields

    [SerializeField]
    protected InventorySlotManager _slotPrefab;

    #endregion

    #region Properties

    public InventorySlotManager SlotPrefab
    {
        get { return _slotPrefab; }
        set { _slotPrefab = value; }
    }

    #endregion

    #region Methods

    private void OnDisable()
    {
        _inventorySystem.OnInventorySlotChanged -= UpdateSlot;
        _inventorySystem.OnInventorySlotChanged -= AssignSlot;
    }

    public void RefreshDynamicInventory(InventorySystem inventoryToDisplay)
    {
        _inventorySystem = inventoryToDisplay;

        _inventorySystem.OnInventorySlotChanged += UpdateSlot;
        _inventorySystem.OnInventorySlotChanged += AssignSlot;

        ClearSlots();
        AssignSlot(_inventorySystem);
    }

    public override void AssignSlot(InventorySlot slot)
    {
        _slotDictionary = new Dictionary<InventorySlotManager, InventorySlot>();

        for (int i = 0; i < transform.childCount; i++)
        {
            var slotToDisplay = transform.GetChild(i).GetComponent<InventorySlotManager>();
            var inventorySlot = _inventorySystem.InventorySlots[i];

            _slotDictionary.Add(slotToDisplay, inventorySlot);
            slotToDisplay.Init(inventorySlot);
        }
    }

    public override void AssignSlot(InventorySystem inventoryToDisplay)
    {
        _slotDictionary = new Dictionary<InventorySlotManager, InventorySlot>();

        if (inventoryToDisplay == null)
            return;

        for (int i = 0; i < inventoryToDisplay.InventorySize; i++)
        {
            var slot = Instantiate(_slotPrefab, transform);
            slot.Init(inventoryToDisplay.InventorySlots[i]);

            _slotDictionary.Add(slot, inventoryToDisplay.InventorySlots[i]);
        }
    }

    private void ClearSlots()
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
