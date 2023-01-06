using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class StaticInventoryDisplay : InventoryDisplay
{

    #region Fields

    [SerializeField]
    private InventoryHolder _inventoryHolder;

    [SerializeField]
    private string _holderName;

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
            switch (_holderName)
            {
                case "hotbar":
                    _inventorySystem = _inventoryHolder.GetComponent<PlayerHotbar>().InventorySystem;
                    break;
                case "inventory":
                    _inventorySystem = _inventoryHolder.GetComponent<PlayerInventory>().InventorySystem;
                    break;
                case "equipment":
                    _inventorySystem = _inventoryHolder.GetComponent<PlayerEquipment>().InventorySystem;
                    break;
                case "backpack":
                    _inventorySystem = _inventoryHolder.GetComponent<PlayerBackpack>().InventorySystem;
                    break;
                default:
                    _inventorySystem = null;
                    break;
            }

            if (_inventorySystem != null)
            {
                _inventorySystem.OnInventorySlotChanged += UpdateSlot;
                AssignSlot(_inventorySystem);
            }
        }
        else
            Debug.LogWarning($"No inventory assigned to {gameObject}");
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
