using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorInventoryHolder : InventoryHolder
{

    #region Fields

    [Header("Static Data")]
    [SerializeField]
    private PlayerData _playerData;

    #endregion

    #region Methods

    private void Awake()
    {
        _inventorySize = _playerData.ArmorInventorySize;
        SetupNewInventorySystem();
    }

    #endregion

}
