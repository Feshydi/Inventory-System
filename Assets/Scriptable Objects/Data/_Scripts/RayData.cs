using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Ray Data")]
public class RayData : ScriptableObject
{

    #region Fields

    [SerializeField]
    private float _rayRange;

    #endregion

    #region Properties

    public float RayRange => _rayRange;

    #endregion

}
