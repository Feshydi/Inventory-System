using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum UIState
{
    Close,                 // All closed
    InventoryOpen,        // Inventory panel opened
    CraftOpen,            // Inventory and craft panels opened
    ContainerOpen         // Inventory and container panels opened
}

public class UIManager : MonoBehaviour
{

    #region Fields

    [Header("Static Data")]
    [SerializeField]
    private InventoryData _inventoryData;

    [Header("Auto Data")]
    [SerializeField]
    private UIState _currentUIState;

    [Header("Auto Data")]
    [SerializeField]
    private List<CanvasGroup> _ui;

    [Header("Customizable Data")]
    [SerializeField]
    private CanvasGroup _background;

    [SerializeField]
    private CanvasGroup _inventory;

    [SerializeField]
    private CanvasGroup _craft;

    [SerializeField]
    private CanvasGroup _container;

    [SerializeField]
    private TextMeshProUGUI _windowTitle;

    [SerializeField]
    private float _fadeDuration;

    [SerializeField]
    private Logger _logger;

    #endregion

    #region Properties

    public UIState CurrentUIState => _currentUIState;

    #endregion

    #region Methods

    private void Start()
    {
        _currentUIState = UIState.Close;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _fadeDuration = 0.3f;

        _background.alpha = 0;
        _inventory.alpha = 0;
        _craft.alpha = 0;
        _container.alpha = 0;

        _background.gameObject.SetActive(false);
        _inventory.gameObject.SetActive(false);
        _craft.gameObject.SetActive(false);
        _container.gameObject.SetActive(false);
    }

    public void ChangeUIStateAndOpen(UIState uIState)
    {
        if (_currentUIState.Equals(UIState.Close))
            Open(uIState);
        else
            StartCoroutine(CLoseAllWindows());
    }

    private void Open(UIState uiState)
    {
        if (IsUITweening(_ui))
            return;

        _ui = GetCanvasGroups(uiState);
        if (_ui.Count == 0)
            return;

        _currentUIState = uiState;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        foreach (var item in _ui)
        {
            item.gameObject.SetActive(true);
            LeanTween.alphaCanvas(item, 1, _fadeDuration);
            _logger.Log($"Showing {item.gameObject}", this);
        }
    }

    private IEnumerator CLoseAllWindows()
    {
        _currentUIState = UIState.Close;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        foreach (var item in _ui)
        {
            LeanTween.alphaCanvas(item, 0, _fadeDuration);
        }
        _logger.Log("Hiding all windows", this);

        yield return new WaitForSeconds(_fadeDuration);

        foreach (var item in _ui)
        {
            item.gameObject.SetActive(false);
        }
        _logger.Log("All windows hided", this);
    }

    private bool IsUITweening(List<CanvasGroup> ui)
    {
        foreach (var item in ui)
        {
            if (LeanTween.isTweening(item.gameObject))
            {
                _logger.Log($"{item.gameObject} is already showing/hiding", this);
                return true;
            }
        }
        return false;
    }

    private List<CanvasGroup> GetCanvasGroups(UIState uiState)
    {
        List<CanvasGroup> ui = new List<CanvasGroup>();
        switch (uiState)
        {
            case UIState.InventoryOpen:
                ui.Add(_background);
                ui.Add(_inventory);
                _windowTitle.text = "Inventory";
                break;
            case UIState.CraftOpen:
                ui.Add(_background);
                ui.Add(_inventory);
                ui.Add(_craft);
                _windowTitle.text = "Crafting";
                break;
            case UIState.ContainerOpen:
                ui.Add(_background);
                ui.Add(_inventory);
                ui.Add(_container);
                _windowTitle.text = "Chest";
                break;
            default:
                _logger.Log("Wrong state", this);
                break;
        }
        return ui;
    }

    #endregion

}
