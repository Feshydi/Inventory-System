using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
    private MouseItem _mouseItem;

    [SerializeField]
    private DynamicInventoryDisplay _dynamicInventory;

    [SerializeField]
    private GameObject _hotbar;

    #endregion

    #region Properties

    public static InventoryController Instance
    {
        get
        {
            if (_instance == null)
                new InventoryController();
            return _instance;
        }
    }

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

    #region Constructors

    private InventoryController()
    {
        _instance = this;
    }

    #endregion

    #region Methods

    private void Awake()
    {
        _dynamicInventory = GetComponentInChildren<DynamicInventoryDisplay>();
        _dynamicInventoryUI.SetActive(false);
        _inventoryUI.SetActive(false);
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
        if (!_dynamicInventoryUI.activeInHierarchy)
        {
            _dynamicInventoryUI.SetActive(true);
            _dynamicInventory.RefreshDynamicInventory(inventoryToDisplay);
            return;
        }

        _dynamicInventoryUI.SetActive(false);
    }

    public void InventorySetActive(bool value)
    {
        GetComponent<Image>().enabled = value;
        _inventoryUI.SetActive(value);
        _backpackUI.SetActive(value);
        _equipmentUI.SetActive(value);
    }

    #endregion

}
