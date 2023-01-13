using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponInventoryHolder : InventoryHolder
{

    #region Fields

    [Header("Static Data")]
    [SerializeField]
    private PlayerData _playerData;

    #endregion

    #region Methods

    private void Awake()
    {
        _inventorySize = _playerData.WeaponInventorySize;
        SetupNewInventorySystem();
    }

    #endregion

}
