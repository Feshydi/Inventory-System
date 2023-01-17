using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Interaction : MonoBehaviour
{

    #region Fields

    [Header("Customizable settings")]
    [SerializeField]
    private TextMeshProUGUI _interaction;

    #endregion

    #region Methods

    private void Awake()
    {
        ShowInteraction("", false);
    }

    public void ShowInteraction(string text, bool status)
    {
        if (status)
        {
            LeanTween.alphaCanvas(GetComponent<CanvasGroup>(), 1, 0.3f);
            _interaction.text = text;
        }
        else
        {
            if (!LeanTween.isTweening(gameObject))
                LeanTween.alphaCanvas(GetComponent<CanvasGroup>(), 0, 0.3f);
        }
    }

    #endregion

}
