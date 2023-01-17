using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Inventory Data")]
public class InventoryData : IData
{

    #region Fields

    [SerializeField]
    private float _startInventoryAlpha;

    [SerializeField]
    private float _openFadeTime;

    [SerializeField]
    private float _closeFadeTime;

    [SerializeField]
    private AnimationCurve _fadeCurve;

    [SerializeField]
    private float _swipeTime;

    [SerializeField]
    private AnimationCurve _swipeCurve;


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

    public float StartInventoryAlpha => _startInventoryAlpha;

    public float OpenFadeTime => _openFadeTime;

    public float CloseFadeTime => _closeFadeTime;

    public AnimationCurve FadeCurve => _fadeCurve;

    public float SwipeTime => _swipeTime;

    public AnimationCurve SwipeCurve => _swipeCurve;


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
