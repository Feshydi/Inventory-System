using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraMovementHolder : MonoBehaviour
{

    #region Fields

    [Header("Static Data")]
    [SerializeField]
    private PlayerData _playerData;

    [SerializeField]
    private Camera _playerCamera;

    [SerializeField]
    private PlayerControls _inputActions;

    [Header("Customizable settings")]
    [SerializeField]
    private float _xRotation;

    #endregion

    #region Methods

    private void Awake()
    {
        _inputActions = new PlayerControls();
    }

    private void OnEnable()
    {
        _inputActions.Player.Enable();
        _inputActions.Player.Camera.performed += Camera_performed;
    }

    private void OnDisable()
    {
        _inputActions.Player.Camera.performed -= Camera_performed;
        _inputActions.Player.Disable();
    }

    private void Camera_performed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (_playerData.IsInventoryOpened)
            return;

        Vector2 inputCamera = context.ReadValue<Vector2>() * Time.deltaTime * _playerData.SensetivityModifier;
        var vertical = inputCamera.y * _playerData.VerticalSensitivity;
        var horizontal = inputCamera.x * _playerData.HorizontalSensitivity;

        _xRotation -= vertical;
        _xRotation = Mathf.Clamp(_xRotation, -_playerData.VerticalRotationRange, _playerData.VerticalRotationRange);
        _playerCamera.transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * horizontal);
    }

    #endregion

}
