using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetObject : MonoBehaviour
{

    #region Fields

    [SerializeField]
    private HelmetType _helmetType;

    #endregion

    #region Properties

    public HelmetType HelmetType
    {
        get { return _helmetType; }
        set { _helmetType = value; }
    }

    #endregion

    #region Methods

    #endregion

}
