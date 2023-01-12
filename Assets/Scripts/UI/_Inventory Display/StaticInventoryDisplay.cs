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
    private Logger _logger;

    #endregion

    #region Methods

    private void Awake()
    {
        if (_inventoryHolder.TryGetComponent(out PlayerWeaponInventoryHolder player))
        {
            if (player.InventorySystem != null)
            {
                player.InventorySystem.OnInventorySlotChanged += UpdateSlot;
                AssignSlots(player.InventorySystem);
            }
        }
        else
            _logger.Log($"No inventory assigned to {gameObject}", this);
    }

    #endregion

}
