using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : ItemObject
{

    #region Fields

    [SerializeField]
    protected List<string> _effectList;

    #endregion

    #region Properties

    public List<string> EffectList => _effectList;

    #endregion

}
