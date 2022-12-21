using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlot
{

    #region Fields

    [SerializeField]
    private int _id;

    [SerializeField]
    private int _stackSize;

    [SerializeField]
    private Dictionary<string, string> _stats;

    #endregion

    #region Properties

    public int ID
    {
        get { return _id; }
    }

    public int StackSize
    {
        get { return _stackSize; }
        set { _stackSize = value; }
    }

    // #to do:
    // Create class for Stats
    public Dictionary<string, string> Stats
    {
        get { return _stats; }
    }

    #endregion

    #region Constructors

    public InventorySlot(ItemObject objectItem, int amount)
    {
        _id = objectItem.ID;
        _stackSize = amount;
        _stats = new Dictionary<string, string>();
    }

    public InventorySlot()
    {
        SetEmptySlot();
    }

    #endregion

    #region Methods

    public void SetSlot(int id, int amount)
    {
        _id = id;
        _stackSize = amount;
        _stats = new Dictionary<string, string>();
    }

    public void SetEmptySlot()
    {
        _id = -1;
        _stackSize = -1;
        _stats = new Dictionary<string, string>();
    }

    public void SetInventorySlotWithItem(ItemObject item, int amount, out int amountLeft)
    {
        int MaxStackSize = GameManager.Instance.Database.GetItem[item.ID].MaxStackSize;
        amountLeft = amount - MaxStackSize;

        _id = item.ID;

        if (amountLeft > 0)
            _stackSize = MaxStackSize;
        else
            _stackSize = amount;
    }

    public bool RoomLeftInStack(int amountToAdd, out int amountRemaining)
    {
        ItemObject item = GameManager.Instance.Database.GetItem[_id];

        amountRemaining = item.MaxStackSize - _stackSize;

        return RoomLeftInStack(amountToAdd);
    }

    public bool RoomLeftInStack(int amountToAdd)
    {
        ItemObject item = GameManager.Instance.Database.GetItem[_id];

        return _stackSize + amountToAdd <= item.MaxStackSize;
    }

    public void AddToStack(int amount)
    {
        _stackSize += amount;
    }

    public override string ToString()
    {
        return GameManager.Instance.Database.GetItem[ID].GetType().Name;
    }

    #endregion

}
