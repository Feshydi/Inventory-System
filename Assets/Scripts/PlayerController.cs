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
    private float _speed = 5.0f;

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

    #endregion

    #region Methods

    private void Awake()
    {
        _controller = gameObject.GetComponent<CharacterController>();
        _inputActions = new PlayerControls();
    }

    private void OnEnable()
    {
        _inputActions?.Enable();
    }

    private void OnDisable()
    {
        _inputActions?.Disable();
    }

    private void Update()
    {

        Vector3 move = new Vector3(_inputActions.Player.Movement.ReadValue<Vector2>().x, 0, _inputActions.Player.Movement.ReadValue<Vector2>().y);
        _controller.Move(move * Time.deltaTime * _speed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
    }

    #endregion

    #region Methods

    public override string ToString()
    {
        return Name.ToString();
    }

    #endregion

}
