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

    public DynamicInventoryDisplay dynamicInventoryDisplay
    {
        get { return _dynamicInventory; }
        set { _dynamicInventory = value; }
    }

    #endregion

    #region Methods

    private void Awake()
    {
        DynamicInventorySetActive(false);
    }

    private void Update()
    {
        if (Keyboard.current.bKey.wasPressedThisFrame)
            DisplayInventory(new InventorySystem(20));

        if (_dynamicInventory.gameObject.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame)
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
        DynamicInventorySetActive(true);
        _dynamicInventory.RefreshDynamicInventory(inventoryToDisplay);
    }

    private void DynamicInventorySetActive(bool value)
    {
        _dynamicInventory.transform.parent.parent.gameObject.SetActive(value);
    }

    #endregion

}
