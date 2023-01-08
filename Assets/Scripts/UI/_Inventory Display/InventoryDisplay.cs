using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public abstract class InventoryDisplay : MonoBehaviour
{

    #region Fields

    [SerializeField]
    protected InventorySystem _inventorySystem;

    [SerializeField]
    protected Dictionary<InventorySlotManager, InventorySlot> _slotDictionary;

    #endregion

    #region Properties

    public InventorySystem InventorySystem => _inventorySystem;

    public Dictionary<InventorySlotManager, InventorySlot> SlotDictionary => _slotDictionary;

    #endregion

    #region Methods

    protected virtual void Start() { }

    public abstract void AssignSlot(InventorySystem inventoryToDisplay);

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

    #endregion

}
