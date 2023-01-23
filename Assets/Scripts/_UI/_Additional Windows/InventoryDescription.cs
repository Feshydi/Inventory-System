using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDescription : Description
{

    #region Fields

    [Header("Customizable settings")]
    [SerializeField]
    private TextMeshProUGUI _name;

    [SerializeField]
    private TextMeshProUGUI _stats;

    [SerializeField]
    private Image _image;

    #endregion

    #region Methods

    public void ShowDescription(string name, string stats, string description, Sprite icon, bool status)
    {
        base.ShowDescription(description, status);

        if (status)
        {
            _name.text = name;
            _stats.text = stats;
            _image.sprite = icon;
        }
    }

    #endregion

}
