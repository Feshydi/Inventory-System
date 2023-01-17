using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayHolder : MonoBehaviour
{

    #region Fields

    [Header("Static Data")]
    [SerializeField]
    private RayData _rayData;

    [Header("Auto settings")]
    [SerializeField]
    private RaycastHit _raycastHit;

    [SerializeField]
    private bool _isHit;

    [SerializeField]
    private GameObject _hitObject;

    [Header("Customizable settings")]
    [SerializeField]
    private Description _description;

    [SerializeField]
    private Interaction _interaction;

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

    public void CastRay()
    {
        var ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward));

        _isHit = Physics.Raycast(ray, out _raycastHit, _rayData.RayRange);
    }

    private void RayHit()
    {
        if (_isHit && !GameManager.Instance.IsInventoryOpened)
        {
            var hitObject = _raycastHit.transform.gameObject;

            if (_hitObject != null)
                if (_hitObject.Equals(hitObject))
                    return;

            if (hitObject.TryGetComponent(out IInteractable interactable))
            {
                _description.ShowDescription(interactable.ToString(), true);
                _interaction.ShowInteraction("e", true);
                _hitObject = hitObject;
                return;
            }
        }

        _description.ShowDescription("", false);
        _interaction.ShowInteraction("", false);
        _hitObject = null;
    }

    #endregion

}
