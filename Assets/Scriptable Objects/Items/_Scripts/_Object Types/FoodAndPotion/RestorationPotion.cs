using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Restoration Potion", menuName = "Inventory System/Items/Potions/Restoration")]
public class RestorationPotion : Potion
{

    #region Fields

    [SerializeField]
    private int _restorationValue;

    [SerializeField]
    private float _restorationTime;

    #endregion

    #region Properties

    public int RestorationValue => _restorationValue;

    public float RestorationTime => _restorationTime;

    #endregion

}
