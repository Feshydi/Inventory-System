using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{

    #region Fields

    [SerializeField]
    protected MouseItem _mouseItem;

    #endregion

    #region Properties

    public MouseItem MouseItem
    {
        get { return _mouseItem; }
        set { _mouseItem = value; }
    }

    #endregion

    #region Methods

    #endregion

}
