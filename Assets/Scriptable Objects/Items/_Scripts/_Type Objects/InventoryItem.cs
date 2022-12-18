using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem
{

    #region Fields

    [SerializeField]
    private int _id;

    [SerializeField]
    private Dictionary<string, string> _stats;

    #endregion

    #region Properties

    public int ID
    {
        get { return _id; }
    }

    // #to do:
    // Create class for Stats
    public Dictionary<string, string> Stats
    {
        get { return _stats; }
    }

    #endregion

    #region Constructors

    public InventoryItem(ObjectItem objectItem)
    {
        _id = objectItem.ID;
        _stats = new Dictionary<string, string>();
    }

    #endregion

    #region Methods

    #endregion

}
