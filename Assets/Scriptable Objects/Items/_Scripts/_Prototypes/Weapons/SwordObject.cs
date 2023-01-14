using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordObject : MonoBehaviour
{

    #region Fields

    [SerializeField]
    private Sword _sword;

    #endregion

    #region Properties

    public Sword Sword
    {
        get { return _sword; }
        set { _sword = value; }
    }

    #endregion

    #region Methods

    public virtual void DamageMessage(PlayerInventoryController target)
    {
        Debug.Log("You hit " + target.ToString());
    }

    #endregion

}
