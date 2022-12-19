using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Boots", menuName = "Inventory System/Items/Equipment/Boots")]
public class BootsType : ItemObject
{

    #region Fields

    [SerializeField]
    private int _defense;

    [SerializeField]
    private int _speed;

    #endregion

    #region Properties

    public int Defense
    {
        get { return _defense; }
        set { _defense = value; }
    }

    public int Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    public BootsType(int defense, int speed)
    {
        _defense = defense;
        _speed = speed;
    }

    #endregion

}
