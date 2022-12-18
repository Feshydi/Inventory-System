using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootsObject : MonoBehaviour
{

    #region Fields

    [SerializeField]
    private BootsType _bootsType;

    #endregion

    #region Properties

    public BootsType BootsType
    {
        get { return _bootsType; }
        set { _bootsType = value; }
    }

    #endregion

    #region Methods

    #endregion

}
