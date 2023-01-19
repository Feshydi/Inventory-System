using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.Logging;
using UnityEngine;

public class CraftKeeper : MonoBehaviour, IInteractable
{

    #region Fields

    [Header("Customizable settings")]
    [SerializeField]
    private CraftingRecipeDisplay _craftDisplay;

    [SerializeField]
    private UIManager _uIManager;

    [SerializeField]
    private CraftSystem _craftSystem;

    [SerializeField]
    private Logger _logger;

    #endregion

    #region Properties

    public CraftSystem CraftSystem => _craftSystem;

    #endregion

    #region Methods

    public void Interact(PlayerInventoryController player)
    {
        _uIManager.ChangeUIStateAndOpen(UIState.CraftOpen);

        if (_uIManager.CurrentUIState.Equals(UIState.CraftOpen))
            _craftDisplay.Init(player, this);
    }

    #endregion

}
