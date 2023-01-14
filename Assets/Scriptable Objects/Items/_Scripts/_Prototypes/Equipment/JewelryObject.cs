using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JewelryObject : MonoBehaviour
{

    #region Fields

    [SerializeField]
    private Jewelry _jewelry;

    #endregion

    #region Properties

    public Jewelry Jewelry
    {
        get { return _jewelry; }
        set { _jewelry = value; }
    }

    #endregion

    #region Methods

    #endregion

}
