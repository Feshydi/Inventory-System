using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordObject : MonoBehaviour
{

    #region Fields

    [SerializeField]
    private SwordType _swordType;

    #endregion

    #region Properties

    public SwordType SwordType
    {
        get { return _swordType; }
        set { _swordType = value; }
    }

    #endregion

    #region Methods

    public virtual void DamageMessage(PlayerInventoryController target)
    {
        Debug.Log("You hit " + target.ToString());
    }

    #endregion

}
