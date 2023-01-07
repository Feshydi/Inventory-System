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
    private float _speed;

    [SerializeField]
    private float _jumpHeight;

    [SerializeField]
    private float _rayRange;

    [SerializeField]
    private Vector3 _velocity;

    [SerializeField]
    private float _gravity;

    [SerializeField]
    private float _gravityModifier;

    [SerializeField]
    private Vector2 _inputMove;

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

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _inputActions = new PlayerControls();

        _speed = 5f;
        _jumpHeight = 1f;
        _rayRange = 2f;
        _velocity = Vector3.zero;
        _gravity = -9.81f;
        _gravityModifier = 0.01f;
        _inputMove = Vector2.zero;
    }

    private void OnEnable()
    {
        _inputActions.Player.Enable();
        _inputActions.Player.Move.performed += Move_Performed;
        _inputActions.Player.Interact.performed += Interact_Performed;
        _inputActions.Player.Jump.performed += Jump_Performed;
        _inputActions.Player.Inventory.performed += Inventory_performed;
    }

    private void OnDisable()
    {
        _inputActions.Player.Move.performed -= Move_Performed;
        _inputActions.Player.Interact.performed -= Interact_Performed;
        _inputActions.Player.Jump.performed -= Jump_Performed;
        _inputActions.Player.Inventory.performed -= Inventory_performed;
        _inputActions.Player.Disable();
    }

    private void Update()
    {
        Move();
        ApplyGravity();
    }

    private void ApplyGravity()
    {
        _isGrounded = _controller.isGrounded;
        if (_isGrounded && _velocity.y < 0)
            _velocity.y = _gravity * _gravityModifier;
        else
            _velocity.y += _gravity * Time.deltaTime;

        _controller.Move(_velocity * Time.deltaTime);
    }

    private void Move()
    {
        if (_isGrounded && !_isInventoryOpened)
        {
            Vector3 move = (transform.right * _inputMove.x + transform.forward * _inputMove.y) * _speed;
            _velocity.x = move.x;
            _velocity.z = move.z;
        }
    }

    private void Move_Performed(InputAction.CallbackContext context)
    {
        _inputMove = context.ReadValue<Vector2>();
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
