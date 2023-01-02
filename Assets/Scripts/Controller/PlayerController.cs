using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private float _sensitivity = 1.0f;

    [SerializeField]
    private float _speed = 5.0f;

    [SerializeField]
    private float _jumpHeight = 1.0f;

    [SerializeField]
    private Vector3 _velocity;

    [SerializeField]
    private float _rotationRange = 75f;

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

    public float Sensitivity
    {
        get { return _sensitivity; }
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

    public float RotationRange
    {
        get { return _rotationRange; }
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

    #endregion

    #region Methods

    private void Awake()
    {
        _controller = gameObject.GetComponent<CharacterController>();
        _inputActions = new PlayerControls();
    }

    private void Update()
    {
        Move();
        Rotate();

        CastRay();
    }

    private void OnEnable()
    {
        _inputActions?.Enable();
    }

    private void OnDisable()
    {
        _inputActions?.Disable();
    }

    private void Move()
    {
        _isGrounded = _controller.isGrounded;
        if (_isGrounded && _velocity.y < 0)
            _velocity.y = 0f;

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
            _velocity.y += Mathf.Sqrt(_jumpHeight * _gravity * -2.0f);

        float moveX = _inputActions.Player.Movement.ReadValue<Vector2>().x;
        float moveZ = _inputActions.Player.Movement.ReadValue<Vector2>().y;
        var move = transform.right * moveX + transform.forward * moveZ;
        _controller.Move(move * Time.deltaTime * _speed);

        _velocity.y += _gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }

    private void Rotate()
    {
        if (_isInventoryOpened)
            return;

        float horizontal = _inputActions.Player.Camera.ReadValue<Vector2>().x;
        float vertical = _inputActions.Player.Camera.ReadValue<Vector2>().y;
        _controller.transform.Rotate(Vector3.up * horizontal * Time.deltaTime * _sensitivity);

        var head = transform.GetChild(0);
        head.Rotate(Vector3.left * vertical * Time.deltaTime * _sensitivity);
        LimitRotation(head, _rotationRange);
    }

    private void LimitRotation(Transform _transform, float range)
    {
        var playerEulerAngles = _transform.rotation.eulerAngles;
        playerEulerAngles.x = (playerEulerAngles.x > 180) ? playerEulerAngles.x - 360 : playerEulerAngles.x;
        playerEulerAngles.x = Mathf.Clamp(playerEulerAngles.x, -range, range);
        _transform.rotation = Quaternion.Euler(playerEulerAngles);
    }

    private void CastRay()
    {
        RaycastHit raycastHit = new RaycastHit();
        bool hit = Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)), out raycastHit, _rayRange);

        if (hit)
        {
            var hitObject = raycastHit.transform.gameObject;

            if (_inputActions.Player.Interact.WasPressedThisFrame() && hitObject.CompareTag("Chest"))
            {
                hitObject.GetComponent<ChestController>().Interact();
                _isInventoryOpened = !_isInventoryOpened;
                Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;
            }
        }
    }

    public override string ToString()
    {
        return Name.ToString();
    }

    #endregion

}
