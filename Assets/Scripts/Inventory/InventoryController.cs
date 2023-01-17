using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class InventoryController : MonoBehaviour
{

    #region Fields

    private static InventoryController _instance;

    [SerializeField]
    private GameObject _dynamicInventoryUI;

    [SerializeField]
    private GameObject _inventoryUI;

    [SerializeField]
    private GameObject _backpackUI;

    [SerializeField]
    private GameObject _equipmentUI;

    [SerializeField]
    private GameObject _hotbarUI;


    [SerializeField]
    private GameObject _craftingUI;

    [SerializeField]
    private MouseItem _mouseItem;

    [SerializeField]
    private DynamicInventoryDisplay _dynamicInventory;

    [SerializeField]
    private GameObject _hotbar;

    #endregion

    #region Properties

    public static InventoryController Instance => _instance == null ? new InventoryController() : _instance;

    public MouseItem MouseItem => _mouseItem;

    public DynamicInventoryDisplay DynamicInventoryDisplay => _dynamicInventory;

    #endregion

    #region Constructors

    private InventoryController() => _instance = this;

    #endregion

    #region Methods

    private void Awake()
    {
        _dynamicInventory = GetComponentInChildren<DynamicInventoryDisplay>();

        _dynamicInventoryUI.SetActive(false);
        _inventoryUI.SetActive(true);
        _backpackUI.SetActive(false);
        _equipmentUI.SetActive(false);
        GetComponent<Image>().enabled = false;
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
        if (!IsDynamicInventoryActive())
        {
            _dynamicInventory.RefreshDynamicInventory(inventoryToDisplay);
            return;
        }

        SetDynamicInventoryActive(false);
    }

    public void SetStaticInventoryActive(bool value)
    {
        // background dim
        GetComponent<Image>().enabled = value;

        // set bool to all static inventories
        _inventoryUI.SetActive(value);
        _backpackUI.SetActive(value);
        _equipmentUI.SetActive(value);
    }

    public void SetDynamicInventoryActive(bool value)
    {
        // set bool to dynamic inventory
        _dynamicInventoryUI.SetActive(value);
    }

    public bool IsStaticInventoryActive()
    {
        return _inventoryUI.activeInHierarchy
            && _backpackUI.activeInHierarchy
            && _equipmentUI.activeInHierarchy;
    }

    public bool IsDynamicInventoryActive()
    {
        return _dynamicInventoryUI.activeInHierarchy;
    }


    public void SetCraftingActive(bool value)
    {
        _craftingUI.SetActive(value);
    }

    #endregion

}
