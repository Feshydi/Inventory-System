using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuDestroyer : MonoBehaviour, IPointerClickHandler
{

    #region Fields

    [SerializeField]
    private ItemMenu _itemMenu;

    #endregion

    #region Methods

    public void OnPointerClick(PointerEventData eventData)
    {
        _itemMenu.Parent.ExitMenu();
    }

    #endregion

}
