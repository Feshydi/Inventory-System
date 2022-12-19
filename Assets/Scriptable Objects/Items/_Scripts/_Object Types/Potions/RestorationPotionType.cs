using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Restoration Potion", menuName = "Inventory System/Items/Potions/Restoration")]
public class RestorationPotionType : ItemObject
{

    #region Fields

    [SerializeField]
    private int _restorationValue;

    [SerializeField]
    private float _restorationTime;

    #endregion

    #region Properties

    public int RestorationValue
    {
        get { return _restorationValue; }
        set { _restorationValue = value; }
    }

    public float RestorationTime
    {
        get { return _restorationTime; }
        set { _restorationTime = value; }
    }

    #endregion

}
