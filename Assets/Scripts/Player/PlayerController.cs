using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
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

    public string Name => _name;

    public bool IsGrounded => _isGrounded;

    public bool IsInventoryOpened => _isInventoryOpened;

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

        _isInventoryOpened = false;
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

        RayHit();
    }

    // apply gravity
    private void ApplyGravity()
    {
        _isGrounded = _controller.isGrounded;
        if (_isGrounded && _velocity.y < 0)
            _velocity.y = _gravity * _gravityModifier;
        else
            _velocity.y += _gravity * Time.deltaTime;

        _controller.Move(_velocity * Time.deltaTime);
    }

    // apply x & z velocity if grounded and inventories closed
    private void Move()
    {
        if (_isGrounded)
        {
            if (!_isInventoryOpened)
            {
                Vector3 move = (transform.right * _inputMove.x + transform.forward * _inputMove.y) * _speed;
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
        if (_isGrounded && !_isInventoryOpened)
            _velocity.y += Mathf.Sqrt(_jumpHeight * _gravity * -2.0f);
    }

    // interact with smth, based on ray
    private void Interact_Performed(InputAction.CallbackContext context)
    {
        if (_isInventoryOpened)
            return;

        if (IsRayHit(out RaycastHit raycastHit))
        {
            var hitObject = raycastHit.transform.gameObject;

            // if ray hits a chest
            if (hitObject.CompareTag("Chest"))
            {
                // open static and dynamic UIs
                hitObject.GetComponent<ChestInventory>().Interact();

                Inventory_performed(context);
            }
            // if ray hits an item
            else if (hitObject.CompareTag("Item"))
            {
                hitObject.GetComponent<GroundItem>().AddItem(GetComponent<Collider>());
            }
            // if ray hits a craft keeper
            else if (hitObject.CompareTag("CraftKeeper"))
            {
                hitObject.GetComponent<CraftKeeper>().Interact();

                Inventory_performed(context);
            }
        }
    }

    // open/close static and close only dynamic inventories
    private void Inventory_performed(InputAction.CallbackContext context)
    {
        // set cursor
        Cursor.lockState = _isInventoryOpened ? CursorLockMode.Locked : CursorLockMode.None;
        // need to fix screen flick after locked

        _isInventoryOpened = !_isInventoryOpened;

        // close static UI
        InventoryController.Instance.SetStaticInventoryActive(_isInventoryOpened);

        // if close static UI, then close other
        if (!_isInventoryOpened)
        {
            InventoryController.Instance.SetDynamicInventoryActive(false);
            InventoryController.Instance.SetCraftingActive(false);
        }
    }

    // update ray check for everything
    private void RayHit()
    {
        if (_isInventoryOpened)
            return;

        if (IsRayHit(out RaycastHit raycastHit))
        {
            var hitObject = raycastHit.transform.gameObject;

            // if ray hits an item
            if (hitObject.CompareTag("Item"))
            {
                InventoryController.Instance.InteractText.text = "e to grab";
                InventoryController.Instance.DescriptionText.text = hitObject.GetComponent<GroundItem>().ObjectItem.ToString();
                InventoryController.Instance.SetInteractTextActive(true);
                InventoryController.Instance.SetDescriptionTextActive(true);
            }
            // if not compare, set default everything
            else
                RayHitDefault();
        }
        // if not hits, set default everything
        else
            RayHitDefault();
    }

    private void RayHitDefault()
    {
        InventoryController.Instance.InteractText.text = "";
        InventoryController.Instance.DescriptionText.text = "";
        InventoryController.Instance.SetInteractTextActive(false);
        InventoryController.Instance.SetDescriptionTextActive(false);
    }

    // check ray hit
    // if hit, then out
    private bool IsRayHit(out RaycastHit raycastHit)
    {
        return Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)), out raycastHit, _rayRange);
    }

    #endregion

}
