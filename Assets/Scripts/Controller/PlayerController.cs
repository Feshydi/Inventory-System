using System.Collections;
using System.Collections.Generic;
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
    private float _rayRange = 1.5f;

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

    public float RayRange
    {
        get { return _rayRange; }
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
        Vector3 move = new Vector3(_inputActions.Player.Movement.ReadValue<Vector2>().x, 0, _inputActions.Player.Movement.ReadValue<Vector2>().y);
        _controller.Move(move * Time.deltaTime * _speed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

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

    private void CastRay()
    {
        RaycastHit raycastHit = new RaycastHit();
        // bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()), out raycastHit, _rayRange);
        Ray ray = new Ray(transform.position, transform.worldToLocalMatrix.MultiplyVector(transform.forward));
        bool hit = Physics.Raycast(ray, out raycastHit, _rayRange);

        if (hit)
        {
            var hitObject = raycastHit.transform.gameObject;

            if (Keyboard.current.eKey.wasPressedThisFrame && hitObject.CompareTag("Chest"))
                hitObject.GetComponent<ChestController>().Interact();
        }
    }

    public override string ToString()
    {
        return Name.ToString();
    }

    #endregion

}
