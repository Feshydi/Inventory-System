using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayHolder : MonoBehaviour
{

    #region Fields

    [SerializeField]
    private RayData _rayData;

    [SerializeField]
    private Ray _ray;

    #endregion

    #region Methods

    private void Awake()
    {
        _ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
    }

    public bool CastRay(out RaycastHit raycastHit)
    {
        var isHit = Physics.Raycast(_ray, out raycastHit, _rayData.RayRange);

        return isHit;
    }

    #endregion

}
