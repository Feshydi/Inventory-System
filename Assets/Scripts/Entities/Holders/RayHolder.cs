using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayHolder : MonoBehaviour
{

    #region Fields

    [Header("Static Data")]
    [SerializeField]
    private RayData _rayData;

    #endregion

    #region Methods

    public bool CastRay(out RaycastHit raycastHit)
    {
        var ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward));

        var isHit = Physics.Raycast(ray, out raycastHit, _rayData.RayRange);

        return isHit;
    }

    #endregion

}
