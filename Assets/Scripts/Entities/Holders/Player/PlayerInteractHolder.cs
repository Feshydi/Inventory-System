using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RayHolder))]
public class PlayerInteractHolder : MonoBehaviour
{

    #region Fields

    [Header("Static Data")]
    [SerializeField]
    private PlayerData _playerData;

    [SerializeField]
    private RayHolder _rayHolder;

    [SerializeField]
    private PlayerControls _inputActions;

    #endregion

    #region Methods

    private void Awake()
    {
        _inputActions.Player.Enable();
        _inputActions.Player.Interact.performed += Interact_performed;
    }

    private void OnDisable()
    {
        _inputActions.Player.Interact.performed -= Interact_performed;
        _inputActions.Player.Disable();
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (_rayHolder.CastRay(out RaycastHit raycastHit))
        {
            var hitObject = raycastHit.transform.gameObject;

            if (hitObject.TryGetComponent(out IInteractable interactable))
            {
                interactable.Interact();
            }
        }
    }

    #endregion

}
