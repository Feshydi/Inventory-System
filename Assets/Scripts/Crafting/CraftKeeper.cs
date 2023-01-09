using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftKeeper : MonoBehaviour, IInteractable
{

    #region Fields

    [SerializeField]
    protected CraftingRecipeDisplay _craftDisplay;

    [SerializeField]
    protected CraftSystem _craftSystem;

    #endregion

    #region Properties

    public CraftSystem CraftSystem => _craftSystem;

    #endregion

    #region Methods

    public virtual void Craft() { }

    public virtual void Interact()
    {
        _craftDisplay.SetCraftSystem(_craftSystem);
    }

    #endregion

}
