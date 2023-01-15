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

    [Header("Inventory")]
    [SerializeField]
    private int _weaponInventorySize;

    [SerializeField]
    private int _bowAndArrowInventorySize;

    [SerializeField]
    private int _shieldInventorySize;

    [SerializeField]
    private int _armorInventorySize;

    [SerializeField]
    private int _materialInventorySize;

    [SerializeField]
    private int _foodInventorySize;

    [SerializeField]
    private int _keyItemsInventorySize;

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


    public float VerticalSensitivity => _verticalSensitivity;

    public float HorizontalSensitivity => _horizontalSensitivity;

    public float SensetivityModifier => _sensetivityModifier;

    public float VerticalRotationRange => _verticalRotationRange;


    public int WeaponInventorySize
    {
        get => _weaponInventorySize;
        set => _weaponInventorySize = value;
    }

    public int BowAndArrowInventorySize
    {
        get => _bowAndArrowInventorySize;
        set => _bowAndArrowInventorySize = value;
    }

    public int ShieldInventorySize
    {
        get => _shieldInventorySize;
        set => _shieldInventorySize = value;
    }

    public int ArmorInventorySize
    {
        get => _armorInventorySize;
        set => _armorInventorySize = value;
    }

    public int MaterialInventorySize
    {
        get => _materialInventorySize;
        set => _materialInventorySize = value;
    }

    public int FoodInventorySize
    {
        get => _foodInventorySize;
        set => _foodInventorySize = value;
    }

    public int KeyItemsInventorySize
    {
        get => _keyItemsInventorySize;
        set => _keyItemsInventorySize = value;
    }

    #endregion

}
