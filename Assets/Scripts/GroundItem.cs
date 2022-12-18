using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundItem : MonoBehaviour
{

    #region Fields

    [SerializeField]
    private ObjectItem _objectItem;

    #endregion

    #region Properties

    public ObjectItem ObjectItem
    {
        get { return _objectItem; }
        set { _objectItem = value; }
    }

    #endregion

}
