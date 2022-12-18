using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestObject : MonoBehaviour
{

    #region Fields

    [SerializeField]
    private ChestType _chestType;

    #endregion

    #region Properties

    public ChestType ChestType
    {
        get { return _chestType; }
        set { _chestType = value; }
    }

    #endregion

    #region Methods

    #endregion

}
