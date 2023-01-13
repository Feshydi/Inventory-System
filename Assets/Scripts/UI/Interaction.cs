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
        _interaction.text = text;
        gameObject.SetActive(status);
    }

    #endregion

}
