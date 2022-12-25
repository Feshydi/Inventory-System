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

    protected override void Start()
    {
        base.Start();

        InventorySystem.OnInventorySlotChanged += UpdateSlot;
    }

    public void RefreshDynamicInventory(InventorySystem inventoryToDisplay)
    {
        InventorySystem = inventoryToDisplay;

        AssignSlot(_inventorySystem);
    }

    public override void AssignSlot(InventorySystem inventoryToDisplay)
    {
        ClearSlots();

        SlotDictionary = new Dictionary<InventorySlotManager, InventorySlot>();

        if (inventoryToDisplay == null)
            return;

        for (int i = 0; i < inventoryToDisplay.InventorySize; i++)
        {
            var slot = Instantiate(_slotPrefab, transform);
            slot.Init(inventoryToDisplay.InventorySlots[i]);

            SlotDictionary.Add(slot, inventoryToDisplay.InventorySlots[i]);
        }
    }

    private void ClearSlots()
    {
        foreach (Transform _transform in transform)
        {
            Destroy(_transform.gameObject);
        }

        if (SlotDictionary != null)
            SlotDictionary.Clear();
    }

    #endregion

}
