using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Boots", menuName = "Inventory System/Items/Equipment/Boots")]
public class Boots : Equipment
{

    #region Fields

    [SerializeField]
    private int _stamina;

    #endregion

    #region Properties

    public int Stamina => _stamina;

    #endregion

    #region Methods

    public override string ToString()
    {
        return string.Concat(
            base.ToString(), "\n",
            "Speed ", _stamina
            );
    }

    #endregion

}
