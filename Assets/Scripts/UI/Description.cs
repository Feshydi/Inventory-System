using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Description : MonoBehaviour
{

    #region Fields

    [SerializeField]
    private TextMeshProUGUI _description;

    #endregion

    #region Methods

    private void Awake()
    {
        BaseItemActions.OnPointedItem += ShowDescription;
    }

    private void ShowDescription(string text, bool status)
    {
        _description.text = text;
        gameObject.SetActive(status);
    }

    #endregion

}
