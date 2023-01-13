using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayHolder : MonoBehaviour
{

    #region Fields

    [Header("Static Data")]
    [SerializeField]
    private RayData _rayData;

    [SerializeField]
    private Description _description;

    [Header("Auto settings")]
    [SerializeField]
    private RaycastHit _raycastHit;

    [SerializeField]
    private Interaction _interaction;

    [SerializeField]
    private bool _isHit;

    #endregion

    #region Properties

    public RaycastHit RaycastHit => _raycastHit;

    public bool IsHit => _isHit;

    #endregion

    #region Methods

    private void Update()
    {
        CastRay();

        RayHit();
    }

    public bool CastRay()
    {
        var ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward));

        _isHit = Physics.Raycast(ray, out _raycastHit, _rayData.RayRange);

        return _isHit;
    }

    private void RayHit()
    {
        if (_isHit)
        {
            var hitObject = _raycastHit.transform.gameObject;

            if (hitObject.TryGetComponent(out IInteractable interactable))
            {
                _description.ShowDescription(interactable.ToString(), true);
                _interaction.ShowInteraction("e", true);
                return;
            }
        }
        RayHitDefault();
    }

    private void RayHitDefault()
    {
        _description.ShowDescription("", false);
        _interaction.ShowInteraction("", false);
    }

    #endregion

}
