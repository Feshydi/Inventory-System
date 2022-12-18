using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeggingsObject : MonoBehaviour
{

    #region Fields

    [SerializeField]
    private LeggingsType _leggingsType;

    #endregion

    #region Properties

    public LeggingsType LeggingsType
    {
        get { return _leggingsType; }
        set { _leggingsType = value; }
    }

    #endregion

    #region Methods

    #endregion

}
