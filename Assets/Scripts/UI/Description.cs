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
        _description.text = text;
        gameObject.SetActive(status);
    }

    #endregion

}
