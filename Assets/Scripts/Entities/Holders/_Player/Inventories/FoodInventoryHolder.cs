using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodInventoryHolder : InventoryHolder
{

    #region Fields

    [Header("Static Data")]
    [SerializeField]
    private PlayerData _playerData;

    #endregion

    #region Methods

    private void Awake()
    {
        _inventorySize = _playerData.FoodInventorySize;
        SetupNewInventorySystem();
    }

    #endregion

}
