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

    public InventoryHolder GetInventoryType(PlayerInventoryController player)
    {
        switch (this)
        {
            case var value when value.GetType().IsSubclassOf(typeof(Sword)):
                if (player.TryGetComponent(out WeaponInventoryHolder weaponHolder))
                    return weaponHolder;
                break;
            //case var value when value.GetType().IsSubclassOf(typeof(BowAndArrow)):
            //    if (player.TryGetComponent(out BowAndArrowInventoryHolder bowAndArrowHolder))
            //        return bowAndArrowHolder;
            //    break;
            //case var value when value.GetType().IsSubclassOf(typeof(Shield)):
            //    if (player.TryGetComponent(out ShieldInventoryHolder shieldHolder))
            //        return shieldHolder;
            //    break;
            case var value when value.GetType().IsSubclassOf(typeof(Equipment)):
                if (player.TryGetComponent(out ArmorInventoryHolder armorHolder))
                    return armorHolder;
                break;
            //case var value when value.GetType().IsSubclassOf(typeof(Material)):
            //    if (player.TryGetComponent(out MaterialInventoryHolder materialHolder))
            //        return materialHolder;
            //    break;
            //case var value when value.GetType().IsSubclassOf(typeof(Food)):
            //case var value1 when value1.GetType().IsSubclassOf(typeof(Potion)):
            case var value when value.GetType() == typeof(RestorationPotion):
                if (player.TryGetComponent(out FoodInventoryHolder foodHolder))
                    return foodHolder;
                break;
            case var value when value.GetType().IsSubclassOf(typeof(Default)):
                if (player.TryGetComponent(out KeyItemsInventoryHolder keyItemsHolder))
                    return keyItemsHolder;
                break;
        }
        return null;
    }

    public override string ToString()
    {
        return "";
    }

    #endregion

}
