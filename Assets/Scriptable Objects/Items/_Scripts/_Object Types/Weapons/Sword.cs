using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sword", menuName = "Inventory System/Items/Weapons/Sword")]
public class Sword : ItemObject
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

    public int AttackDamage => _attackDamage;

    public float AttackRate => _attackRate;

    public float AttackRange => _attackRange;

    #endregion

}
