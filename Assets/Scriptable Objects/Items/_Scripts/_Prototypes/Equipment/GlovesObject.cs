using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlovesObject : MonoBehaviour
{

    #region Fields

    [SerializeField]
    private GlovesType _glovesType;

    #endregion

    #region Properties

    public GlovesType GlovesType
    {
        get { return _glovesType; }
        set { _glovesType = value; }
    }

    #endregion

    #region Methods

    #endregion

}
