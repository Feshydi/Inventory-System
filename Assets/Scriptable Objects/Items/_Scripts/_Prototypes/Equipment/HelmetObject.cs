using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetObject : MonoBehaviour
{

    #region Fields

    [SerializeField]
    private Helmet _helmet;

    #endregion

    #region Properties

    public Helmet Helmet
    {
        get { return _helmet; }
        set { _helmet = value; }
    }

    #endregion

    #region Methods

    #endregion

}
