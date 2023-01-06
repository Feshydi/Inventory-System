using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{

    #region Fields

    [SerializeField]
    private CharacterController _controller;

    [SerializeField]
    private PlayerControls _inputActions;

    [SerializeField]
    private string _name;

    [SerializeField]
    private float _speed = 5.0f;

    [SerializeField]
    private float _jumpHeight = 1.0f;

    [SerializeField]
    private Vector3 _velocity;

    [SerializeField]
    private float _gravity = -9.81f;

    [SerializeField]
    private float _rayRange = 1.5f;

    [SerializeField]
    private bool _isGrounded;

    [SerializeField]
    private bool _isInventoryOpened;

    #endregion

    #region Properties

    public string Name
    {
        get { return _name; }
    }

    public float Speed
    {
        get { return _speed; }
    }

    public float JumpHeight
    {
        get { return _jumpHeight; }
    }

    public Vector3 Velocity
    {
        get { return _velocity; }
    }

    public float Gravity
    {
        get { return _gravity; }
    }

    public float RayRange
    {
        get { return _rayRange; }
    }

    public bool IsGrounded
    {
        get { return _isGrounded; }
    }

    public bool IsInventoryOpened
    {
        get { return _isInventoryOpened; }
    }

    #endregion

    #region Methods

    private void OnEnable()
    {
        _controller = GetComponent<CharacterController>();
        _inputActions = new PlayerControls();
        _inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Player.Disable();
    }

    private void Start()
    {
        _inputActions.Player.Move.performed += Move_Performed;
        _inputActions.Player.Interact.performed += Interact_Performed;
        _inputActions.Player.Jump.performed += Jump_Performed;
        _inputActions.Player.Inventory.performed += Inventory_performed;
    }

    private void Update()
    {
        ApplyGravity();
    }

    private void ApplyGravity()
    {
        _isGrounded = _controller.isGrounded;
        if (_isGrounded && _velocity.y < 0)
            _velocity.y = 0f;

        _velocity.y += _gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }

    private void Move_Performed(InputAction.CallbackContext context)
    {
        if (_isInventoryOpened)
            return;

        Vector2 inputMove = context.ReadValue<Vector2>() * Time.deltaTime;
        var move = transform.right * inputMove.x + transform.forward * inputMove.y;
        _controller.Move(move * _speed);
    }

    private void Jump_Performed(InputAction.CallbackContext context)
    {
        if (_isGrounded && !_isInventoryOpened)
            _velocity.y += Mathf.Sqrt(_jumpHeight * _gravity * -2.0f);
    }

    private void Interact_Performed(InputAction.CallbackContext context)
    {
        if (isRayHit(out RaycastHit raycastHit))
        {
            var hitObject = raycastHit.transform.gameObject;

            if (hitObject.CompareTag("Chest"))
            {
                hitObject.GetComponent<ChestInventory>().Interact();

                Inventory_performed(context);
            }
            else if (hitObject.CompareTag("Item"))
            {
                hitObject.GetComponent<GroundItem>().AddItem(GetComponent<Collider>());
            }
        }
    }

    private void Inventory_performed(InputAction.CallbackContext context)
    {
        _isInventoryOpened = !_isInventoryOpened;
        InventoryController.Instance.InventorySetActive(_isInventoryOpened);
        Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;
    }

    private bool isRayHit(out RaycastHit raycastHit)
    {
        return Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)), out raycastHit, _rayRange);
    }

    public override string ToString()
    {
        return Name.ToString();
    }

    #endregion

}
