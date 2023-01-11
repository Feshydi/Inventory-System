using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Player Data")]
public class PlayerData : IData
{

    #region Fields

    [Header("General")]
    [SerializeField]
    private string _name;

    [Header("Movement")]
    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _jumpHeight;

    [SerializeField]
    private float _gravity;

    [SerializeField]
    private float _gravityModifier;

    [SerializeField]
    private bool _isGrounded;

    [SerializeField]
    private bool _isInventoryOpened;

    [Header("Camera")]
    [SerializeField]
    [Range(0.01f, 1f)]
    private float _verticalSensitivity;

    [SerializeField]
    [Range(0.01f, 1f)]
    private float _horizontalSensitivity;

    [SerializeField]
    private float _sensetivityModifier;

    [SerializeField]
    private float _verticalRotationRange;

    #endregion

    #region Properties

    public string Name
    {
        get => _name;
        set => _name = value;
    }


    public float Speed => _speed;

    public float JumpHeight => _jumpHeight;

    public float Gravity => _gravity;

    public float GravityModifier => _gravityModifier;

    public bool IsGrounded
    {
        get => _isGrounded;
        set => _isGrounded = value;
    }

    public bool IsInventoryOpened
    {
        get => _isInventoryOpened;
        set => _isInventoryOpened = value;
    }


    public float VerticalSensitivity => _verticalSensitivity;

    public float HorizontalSensitivity => _horizontalSensitivity;

    public float SensetivityModifier => _sensetivityModifier;

    public float VerticalRotationRange => _verticalRotationRange;

    #endregion

}
