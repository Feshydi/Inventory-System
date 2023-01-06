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
    private float _sensitivity = 1.0f;

    [SerializeField]
    private float _rotationRange = 75f;

    [SerializeField]
    private float _xRotation = 0f;

    #endregion

    #region Properties

    public float Sensitivity
    {
        get { return _sensitivity; }
    }

    public float RotationRange
    {
        get { return _rotationRange; }
    }

    #endregion

    #region Methods

    private void OnEnable()
    {
        _inputActions = new PlayerControls();
        _inputActions.Player.Enable();

        _inputActions.Player.Look.performed += Look_performed;
    }

    private void OnDisable()
    {
        _inputActions.Player.Disable();
    }

    private void Look_performed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (_playerController.IsInventoryOpened)
            return;

        Vector2 inputLook = context.ReadValue<Vector2>() * _sensitivity * Time.deltaTime;

        _xRotation -= inputLook.y;
        _xRotation = Mathf.Clamp(_xRotation, -_rotationRange, _rotationRange);

        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        _playerController.transform.Rotate(Vector3.up * inputLook.x);

        //transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(new Vector3(inputLook.x, 0f, 0f)), Time.deltaTime * _sensitivity);

        //var head = transform.GetChild(0);
        //head.rotation = Quaternion.Slerp(head.rotation, Quaternion.LookRotation(inputLook), Time.deltaTime * _sensitivity);
        //LimitRotation(head, _rotationRange);
    }

    private void LimitRotation(Transform _transform, float range)
    {
        var playerEulerAngles = _transform.rotation.eulerAngles;
        playerEulerAngles.x = (playerEulerAngles.x > 180) ? playerEulerAngles.x - 360 : playerEulerAngles.x;
        playerEulerAngles.x = Mathf.Clamp(playerEulerAngles.x, -range, range);
        _transform.rotation = Quaternion.Euler(playerEulerAngles);
    }

    #endregion

}
