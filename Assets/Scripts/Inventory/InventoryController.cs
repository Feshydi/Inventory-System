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
    private GameObject _descriptionUI;

    [SerializeField]
    private GameObject _interactUI;

    [SerializeField]
    private MouseItem _mouseItem;

    [SerializeField]
    private TextMeshProUGUI _descriptionText;

    [SerializeField]
    private TextMeshProUGUI _interactText;

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

    public TextMeshProUGUI DescriptionText
    {
        get { return _descriptionText; }
        set { _descriptionText = value; }
    }

    public TextMeshProUGUI InteractText
    {
        get { return _interactText; }
        set { _interactText = value; }
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

        _descriptionUI.SetActive(false);
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
            SetDynamicInventoryActive(true);
            SetStaticInventoryActive(true);
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

    public void SetDescriptionTextActive(bool value)
    {
        _descriptionUI.SetActive(value);
    }

    public void SetInteractTextActive(bool value)
    {
        _interactUI.SetActive(value);
    }

    #endregion

}
