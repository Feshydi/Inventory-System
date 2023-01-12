using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponInventoryHolder : InventoryHolder
{

    #region Fields

    [SerializeField]
    private PlayerData _playerData;

    #endregion

    #region Methods

    protected override void Awake()
    {
        _inventorySize = _playerData.WeaponInventorySize;

        base.Awake();
    }

    #endregion

}
