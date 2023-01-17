using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Description : MonoBehaviour
{

    #region Fields

    [Header("Customizable settings")]
    [SerializeField]
    private TextMeshProUGUI _description;

    #endregion

    #region Methods

    private void Awake()
    {
        ShowDescription("", false);
    }

    public void ShowDescription(string text, bool status)
    {
        if (status)
        {
            LeanTween.alphaCanvas(GetComponent<CanvasGroup>(), 1, 0.3f);
            _description.text = text;
        }
        else
        {
            if (!LeanTween.isTweening(gameObject))
                LeanTween.alphaCanvas(GetComponent<CanvasGroup>(), 0, 0.3f);
        }
    }

    #endregion

}
