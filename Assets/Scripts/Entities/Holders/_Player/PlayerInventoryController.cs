using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;

public class PlayerInventoryController : MonoBehaviour
{

    #region Fields

    [Header("Static Data")]
    [SerializeField]
    private InventoryData _inventoryData;

    [Header("Auto Settings")]
    [SerializeField]
    private PlayerControls _inputActions;

    [SerializeField]
    private List<InventoryHolder> _inventoryHolders;

    [Header("Customizable settings")]
    [SerializeField]
    private CanvasGroup _inventoryUI;

    [SerializeField]
    private List<CanvasGroup> _canvasGroupUI;

    [SerializeField]
    private Logger _logger;

    #endregion

    #region Properties

    public List<InventoryHolder> InventoryHolders => _inventoryHolders;

    public CanvasGroup InventoryUI => _inventoryUI;

    #endregion

    #region Methods

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        LeanTween.alphaCanvas(_inventoryUI, _inventoryData.StartInventoryAlpha, 0.1f);

        GetInventoryHolders();

        _inputActions = new PlayerControls();
    }

    private void OnEnable()
    {
        _inputActions.Player.Enable();
        _inputActions.Player.Inventory.performed += Inventory_performed;
    }

    private void OnDisable()
    {
        _inputActions.Player.Inventory.performed -= Inventory_performed;
        _inputActions.Player.Disable();
    }

    private void Inventory_performed(InputAction.CallbackContext context)
    {
        if (LeanTween.isTweening(_inventoryUI.gameObject))
        {
            _logger.Log("Inventory is already showing/hiding", this);
            return;
        }
        ChangeInventoryUIAlpha();
    }

    public void ChangeInventoryUIAlpha()
    {
        var managerInstance = GameManager.Instance;

        managerInstance.InventoryStatusChange();
        if (managerInstance.IsInventoryOpened)
        {
            LeanTween.alphaCanvas(_inventoryUI, 1, _inventoryData.OpenFadeTime);
            _logger.Log($"Showing {_inventoryUI}", this);
        }
        else
        {
            foreach (var item in _canvasGroupUI)
            {
                LeanTween.alphaCanvas(item, 0, _inventoryData.CloseFadeTime);
                _logger.Log($"Hiding {item}", this);
            }
        }

        Cursor.lockState = managerInstance.IsInventoryOpened ? CursorLockMode.None : CursorLockMode.Locked;
    }

    private void GetInventoryHolders()
    {
        _inventoryHolders = new List<InventoryHolder>();
        foreach (var component in GetComponents<InventoryHolder>())
        {
            _inventoryHolders.Add(component);
        }
    }

    public InventoryHolder GetPreviousInventoryHolder(InventoryHolder previousOfThis)
    {
        return
            _inventoryHolders
            .TakeWhile(x => x != previousOfThis)
            .DefaultIfEmpty(_inventoryHolders[0])
            .LastOrDefault();
    }

    public InventoryHolder GetNextInventoryHolder(InventoryHolder nextOfThis)
    {
        return
            _inventoryHolders
            .SkipWhile(x => x != nextOfThis).Skip(1)
            .DefaultIfEmpty(_inventoryHolders[_inventoryHolders.Count - 1])
            .FirstOrDefault();
    }

    #endregion

}
