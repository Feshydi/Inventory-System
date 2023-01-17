using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.Logging;
using UnityEngine;

public class CraftKeeper : MonoBehaviour, IInteractable
{

    #region Fields

    [Header("Customizable settings")]
    [SerializeField]
    protected CraftingRecipeDisplay _craftDisplay;

    [SerializeField]
    protected CraftSystem _craftSystem;

    [SerializeField]
    private CanvasGroup _craftUI;

    [SerializeField]
    private Logger _logger;

    #endregion

    #region Properties

    public CraftSystem CraftSystem => _craftSystem;

    #endregion

    #region Methods

    private void Awake()
    {
        LeanTween.alphaCanvas(_craftUI, 0, 0.1f);
    }

    public virtual void Craft() { }

    public void Interact(PlayerInventoryController player)
    {
        _craftDisplay.Init(player, this);

        ChangeCraftUIAlpha();
    }

    public void Interact()
    {
        throw new System.NotImplementedException();
    }

    public void ChangeCraftUIAlpha()
    {
        var managerInstance = GameManager.Instance;

        if (managerInstance.IsInventoryOpened)
        {
            LeanTween.alphaCanvas(_craftUI, 1, 0.3f);
            _logger.Log($"Showing {_craftUI}", this);
        }
    }

    #endregion

}
