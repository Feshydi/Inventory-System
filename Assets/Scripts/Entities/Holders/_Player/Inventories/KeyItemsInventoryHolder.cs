﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItemsInventoryHolder : InventoryHolder
{

    #region Fields

    [Header("Static Data")]
    [SerializeField]
    private InventoryData _inventoryData;

    #endregion

    #region Methods

    private void Awake()
    {
        _inventorySize = _inventoryData.KeyItemsInventorySize;
        SetupNewInventorySystem();
    }

    #endregion

}
