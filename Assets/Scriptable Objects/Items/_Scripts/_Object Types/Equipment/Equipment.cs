using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : ItemObject
{

    #region Fields

    [SerializeField]
    private int _defense;

    #endregion

    #region Properties

    public int Defense => _defense;

    #endregion

    #region Methods

    public override string ToString()
    {
        return string.Concat(
            base.ToString(),
            "Defense: ", _defense
            );
    }

    #endregion

}
