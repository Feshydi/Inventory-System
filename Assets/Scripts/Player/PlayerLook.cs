using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{

    #region Fields

    [SerializeField]
    private PlayerControls _inputActions;

    [SerializeField]
    private PlayerController _playerController;

    [SerializeField]
    [Range(0.01f, 1f)]
    private float _verticalSensitivity;

    [SerializeField]
    [Range(0.01f, 1f)]
    private float _horizontalSensitivity;

    [SerializeField]
    private float _rotationRange;

    [SerializeField]
    private float _xRotation;

    [SerializeField]
    private float _sensetivityModifier;
    #endregion

    #region Properties

    public float VerticalSensitivity
    {
        get { return _verticalSensitivity; }
        set { _verticalSensitivity = value; }
    }

    public float HorizontalSensitivity
    {
        get { return _horizontalSensitivity; }
        set { _horizontalSensitivity = value; }
    }

    #endregion

    #region Methods

    private void Awake()
    {
        _inputActions = new PlayerControls();

        _sensetivityModifier = 10f;
        _xRotation = 0f;
    }

    private void OnEnable()
    {
        _inputActions.Player.Enable();
        _inputActions.Player.Look.performed += Look_performed;
    }

    private void OnDisable()
    {
        _inputActions.Player.Look.performed -= Look_performed;
        _inputActions.Player.Disable();
    }

    private void Look_performed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (_playerController.IsInventoryOpened)
            return;

        Vector2 inputLook = context.ReadValue<Vector2>();

        _xRotation -= inputLook.y * _verticalSensitivity * Time.deltaTime * _sensetivityModifier;
        _xRotation = Mathf.Clamp(_xRotation, -_rotationRange, _rotationRange);
        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        _playerController.transform.Rotate(Vector3.up * inputLook.x * _horizontalSensitivity * Time.deltaTime * _sensetivityModifier);
    }

    #endregion

}
