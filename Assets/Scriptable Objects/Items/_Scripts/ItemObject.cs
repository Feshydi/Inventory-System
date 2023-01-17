using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class used just as list of item types
// #to do:
// Create all the type and object classes
public class TypeItem
{
    public enum ItemType
    {

        // Equipment
        Helmet,
        Chest,
        Gloves,
        Leggings,
        Boots,
        Jewelry,
        Ring,

        // Weapons
        Sword,

        // Tools
        Axe,
        Pickaxe,
        Mallet,

        // Should be increased
        Potion,
        HPRestorationPotion,

        Food,

        Default,

    }
}

[System.Serializable]
public abstract class ItemObject : ScriptableObject
{

    #region Fields

    [SerializeField]
    private int _id;

    [SerializeField]
    private Sprite _icon;

    [SerializeField]
    private string _title;

    [SerializeField]
    [TextArea(15, 20)]
    private string _description;

    [SerializeField]
    private int _maxStackSize;

    #endregion

    #region Properties

    public int ID
    {
        get { return _id; }
        set { _id = value; }
    }

    public Sprite Icon
    {
        get { return _icon; }
        set { _icon = value; }
    }

    public string Title
    {
        get { return _title; }
        set { _title = value; }
    }

    public string Description
    {
        get { return _description; }
        set { _description = value; }
    }

    public int MaxStackSize
    {
        get { return _maxStackSize; }
        set { _maxStackSize = value; }
    }

    #endregion

    #region Methods

    public override string ToString()
    {
        return "";
    }

    #endregion

}
