using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JewelryObject : MonoBehaviour
{

    #region Fields

    [SerializeField]
    private JewelryType _jewelryType;

    #endregion

    #region Properties

    public JewelryType JewelryType
    {
        get { return _jewelryType; }
        set { _jewelryType = value; }
    }

    #endregion

    #region Methods

    #endregion

}
