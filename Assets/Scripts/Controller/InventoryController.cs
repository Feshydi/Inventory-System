using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryController : MonoBehaviour
{

    #region Fields

    [SerializeField]
    private MouseItem _mouseItem;

    [SerializeField]
    private DynamicInventoryDisplay _dynamicInventory;

    #endregion

    #region Properties

    public MouseItem MouseItem
    {
        get { return _mouseItem; }
        set { _mouseItem = value; }
    }

    public DynamicInventoryDisplay DynamicInventoryDisplay
    {
        get { return _dynamicInventory; }
    }

    #endregion

    #region Methods

    private void Awake()
    {
        DynamicInventorySetActive(false);
    }

    private void OnEnable()
    {
        InventoryHolder.OnDynamicInventoryDisplayRequested += DisplayInventory;
    }

    private void OnDisable()
    {
        InventoryHolder.OnDynamicInventoryDisplayRequested -= DisplayInventory;
    }

    private void DisplayInventory(InventorySystem inventoryToDisplay)
    {
        if (!_dynamicInventory.gameObject.activeInHierarchy)
        {
            DynamicInventorySetActive(true);
            _dynamicInventory.RefreshDynamicInventory(inventoryToDisplay);
            return;
        }

        DynamicInventorySetActive(false);
    }

    private void DynamicInventorySetActive(bool value)
    {
        _dynamicInventory.transform.parent.parent.gameObject.SetActive(value);
    }

    #endregion

}
