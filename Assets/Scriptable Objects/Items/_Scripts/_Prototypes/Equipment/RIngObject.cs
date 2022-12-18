using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingObject : MonoBehaviour
{

    #region Fields

    [SerializeField]
    private RingType _ringType;

    #endregion

    #region Properties

    public RingType RingType
    {
        get { return _ringType; }
        set { _ringType = value; }
    }

    #endregion

    #region Methods

    #endregion

}
