using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementHolder : MonoBehaviour
{

    #region Fields

    [Header("Static Data")]
    [SerializeField]
    private PlayerData _playerData;

    [Header("Autosettings")]
    [SerializeField]
    private CharacterController _characterController;

    [SerializeField]
    private PlayerControls _inputActions;

    [Header("Customizable settings")]
    [SerializeField]
    private UIManager _uIManager;

    [SerializeField]
    private Vector2 _inputMove;

    [SerializeField]
    private Vector3 _velocity;

    [SerializeField]
    private bool _isGrounded;

    #endregion

    #region Methods

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();

        _inputMove = Vector2.zero;
        _velocity = Vector3.zero;

        _inputActions = new PlayerControls();
    }

    private void OnEnable()
    {
        _inputActions.Player.Enable();
        _inputActions.Player.Move.performed += Move_Performed;
        _inputActions.Player.Jump.performed += Jump_Performed;
    }

    private void OnDisable()
    {
        _inputActions.Player.Move.performed -= Move_Performed;
        _inputActions.Player.Jump.performed -= Jump_Performed;
        _inputActions.Player.Disable();
    }

    private void Update()
    {
        ApplyVelocity();
    }

    private void ApplyVelocity()
    {
        _isGrounded = _characterController.isGrounded;

        InputVelocity();

        GravityVelocity();

        _characterController.Move(_velocity * Time.deltaTime);
    }

    // apply gravity
    private void GravityVelocity()
    {
        if (_isGrounded && _velocity.y < 0)
            _velocity.y = _playerData.Gravity * _playerData.GravityModifier;
        else
            _velocity.y += _playerData.Gravity * Time.deltaTime;
    }

    // apply x & z velocity if grounded and inventories closed
    private void InputVelocity()
    {
        if (_isGrounded)
        {
            if (_uIManager.CurrentUIState.Equals(UIState.Close))
            {
                Vector3 move = (transform.right * _inputMove.x + transform.forward * _inputMove.y) * _playerData.Speed;
                _velocity.x = move.x;
                _velocity.z = move.z;
            }
            else
            {
                _velocity.x = 0;
                _velocity.z = 0;
            }
        }
    }

    // read move input
    private void Move_Performed(InputAction.CallbackContext context)
    {
        _inputMove = context.ReadValue<Vector2>();
    }

    // if grounded and closed inventories, then jump
    private void Jump_Performed(InputAction.CallbackContext context)
    {
        if (_isGrounded && _uIManager.CurrentUIState.Equals(UIState.Close))
            _velocity.y += Mathf.Sqrt(_playerData.JumpHeight * _playerData.Gravity * -2.0f);
    }

    #endregion

}
