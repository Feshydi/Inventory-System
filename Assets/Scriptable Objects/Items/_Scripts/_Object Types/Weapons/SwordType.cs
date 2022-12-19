using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sword", menuName = "Inventory System/Items/Weapons/Sword")]
public class SwordType : ItemObject
{

    #region Fields

    [SerializeField]
    private int _attackDamage;

    [SerializeField]
    private float _attackRate;

    [SerializeField]
    private float _attackRange;

    #endregion

    #region Properties

    public int AttackDamage
    {
        get { return _attackDamage; }
        set { _attackDamage = value; }
    }

    public float AttackRate
    {
        get { return _attackRate; }
        set { _attackRate = value; }
    }

    public float AttackRange
    {
        get { return _attackRange; }
        set { _attackRange = value; }
    }

    #endregion

}
