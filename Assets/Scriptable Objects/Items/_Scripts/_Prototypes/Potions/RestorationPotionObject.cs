using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestorationPotionObject : MonoBehaviour
{

    #region Fields

    [SerializeField]
    private RestorationPotionType _restorationPotionType;

    #endregion

    #region Properties

    public RestorationPotionType RestorationPotionType
    {
        get { return _restorationPotionType; }
        set { _restorationPotionType = value; }
    }

    #endregion

    #region Methods

    #endregion

}
