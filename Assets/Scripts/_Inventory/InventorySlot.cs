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
    private bool _pointed;

    #endregion

    #region Properties

    public int ID => _id;

    public int StackSize
    {
        get => _stackSize;
        set => _stackSize = value;
    }

    public bool Pointed
    {
        get => _pointed;
        set => _pointed = value;
    }

    #endregion

    #region Constructors

    public InventorySlot(ItemObject objectItem, int amount) => SetSlot(objectItem.ID, amount);

    public InventorySlot(int id, int amount) => SetSlot(id, amount);

    public InventorySlot() => SetEmptySlot();

    #endregion

    #region Methods

    public void SetEmptySlot()
    {
        _id = -1;
        _stackSize = -1;
    }

    public void SetSlot(InventorySlot slot)
    {
        _id = slot._id;
        _stackSize = slot._stackSize;
    }

    public void SetSlot(int id, int amount)
    {
        _id = id;
        _stackSize = amount;
    }

    public void SetSlot(ItemObject item, int amount, out int amountLeft)
    {
        SetSlot(item.ID, amount < item.MaxStackSize ? amount : item.MaxStackSize);

        amountLeft = amount - item.MaxStackSize;
    }

    public void AddToStack(int amount) => _stackSize += amount;

    public void removeFromStack(int amount) => _stackSize -= amount;

    public bool RoomLeftInStack(int amountToAdd, out int roomLeft)
    {
        ItemObject item = GameManager.Instance.Database.GetItem[_id];

        roomLeft = item.MaxStackSize - _stackSize;

        return _stackSize + amountToAdd <= item.MaxStackSize;
    }

    public override string ToString()
    {
        return GameManager.Instance.Database.GetItem[ID].GetType().Name;
    }

    #endregion

}
