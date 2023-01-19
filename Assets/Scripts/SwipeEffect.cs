using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeEffect : MonoBehaviour
{
    #region Fields

    [Header("Static Data")]
    [SerializeField]
    private InventoryData _inventoryData;

    [Header("Auto settings")]
    [SerializeField]
    private float _swipeDuration;

    [SerializeField]
    private AnimationCurve _swipeCurve;

    [SerializeField]
    private Vector2 _startPosition;

    [SerializeField]
    private Vector2 _currentPosition;

    [SerializeField]
    private Vector2 _swipeDistance;

    #endregion

    #region Methods

    private void Start()
    {
        _swipeDuration = _inventoryData.SwipeTime;
        _swipeCurve = _inventoryData.SwipeCurve;
        _startPosition = transform.position;
        _currentPosition = _startPosition;

        _swipeDistance = GetComponent<RectTransform>().sizeDelta;
        _swipeDistance.x -= GetComponent<HorizontalLayoutGroup>().padding.left * 2;
        _swipeDistance.x += GetComponent<HorizontalLayoutGroup>().spacing;
    }


    public void Swipe(bool right)
    {
        if (right)
            _currentPosition.x += _swipeDistance.x;
        else
            _currentPosition.x -= _swipeDistance.x;

        LeanTween
            .moveX(gameObject, _currentPosition.x, _swipeDuration)
            .setEaseInOutQuad();
    }

    #endregion

}

