using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractHolder : MonoBehaviour
{

    #region Fields

    [Header("Auto Settings")]
    [SerializeField]
    private PlayerControls _inputActions;

    [Header("Customizable settings")]
    [SerializeField]
    private RayHolder _rayHolder;

    [SerializeField]
    private Logger _logger;

    #endregion

    #region Methods

    private void Awake()
    {
        _inputActions = new PlayerControls();
    }

    private void OnEnable()
    {
        _inputActions.Player.Enable();
        _inputActions.Player.Interact.performed += Interact_performed;
    }

    private void OnDisable()
    {
        _inputActions.Player.Interact.performed -= Interact_performed;
        _inputActions.Player.Disable();
    }

    // interact with smth, based on ray
    private void Interact_performed(InputAction.CallbackContext context)
    {
        if (_rayHolder.IsHit)
        {
            var hitObject = _rayHolder.RaycastHit.transform.gameObject;

            if (hitObject.TryGetComponent(out IInteractable interactable))
            {
                GetComponent<PlayerInventoryController>().ChangeInventoryUIAlpha();

                _logger.Log($"Interacting with {interactable}", this);
                interactable.Interact(GetComponent<PlayerInventoryController>());
            }
        }
    }

    #endregion

}
