using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour, IInteractable
{

    #region Fields

    [SerializeField]
    private string _name;

    #endregion

    #region Properties

    public string Name
    {
        get { return _name; }
    }

    #endregion

    #region Methods


    public void Interact()
    {
        InventoryHolder inventoryHolder = GetComponent<InventoryHolder>();
        InventoryHolder.OnDynamicInventoryDisplayRequested.Invoke(inventoryHolder.InventorySystem);
    }

    #endregion

}
