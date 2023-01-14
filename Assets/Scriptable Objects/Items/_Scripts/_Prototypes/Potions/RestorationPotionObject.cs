using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestorationPotionObject : MonoBehaviour
{

    #region Fields

    [SerializeField]
    private RestorationPotion _restorationPotionType;

    #endregion

    #region Properties

    public RestorationPotion RestorationPotion
    {
        get { return _restorationPotionType; }
        set { _restorationPotionType = value; }
    }

    #endregion

    #region Methods

    #endregion

}
