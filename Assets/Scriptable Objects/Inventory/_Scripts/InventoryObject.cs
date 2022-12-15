using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class InventorySlot {
    public int ID = -1;
    public Item item;
    [Range(1, 99)]
    public int amount;
    private int maxStackAmount;

    public InventorySlot() {
        ID = -1;
        item = null;
        amount = 0;
        maxStackAmount = 0;
    }

    public InventorySlot(int _id, Item _item, int _amount, int _maxStackAmount) {
        ID = _id;
        item = _item;
        amount = _amount;
        maxStackAmount = _maxStackAmount;
    }

    public void UpdateSlot(int _id, Item _item, int _amount, int _maxStackAmount) {
        ID = _id;
        item = _item;
        amount = _amount;
        maxStackAmount = _maxStackAmount;
    }

    public void AddAmount(int value) {
        amount += value;
    }
}

[System.Serializable]
public class Inventory {
    public int capacity = 15;
    public InventorySlot[] Items;

    public Inventory() {
        Items = new InventorySlot[capacity];
    }
}

[CreateAssetMenu(menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject {
    public string savePath;
    public ItemDatabaseObject database;
    public Inventory Container = new Inventory();

    public void AddItem(Item _item, int _amount) {
        foreach (var slot in Container.Items) {
            if (slot.ID == _item.Id && slot.amount < slot.item.MaxStack) {
                slot.AddAmount(_amount);
                return;
            }
        }
        SetEmptySLot(_item, _amount);
    }

    private InventorySlot SetEmptySLot(Item _item, int _amount) {
        foreach (var slot in Container.Items) {
            if (slot.ID <= -1) {
                slot.UpdateSlot(_item.Id, _item, _amount, _item.MaxStack);
                return slot;
            }
        }
        return null;
    }

    public bool IsFull(Item item) {
        foreach (var slot in Container.Items) {
            if (slot.ID <= -1 || item.Id == slot.item.Id && slot.amount < slot.item.MaxStack) {
                return false;
            }
        }
        return true;
    }

    public void SwapItems(InventorySlot _slot1, InventorySlot _slot2) {
        int ind1 = 0, ind2 = 0;
        for (int i = 0; i < Container.Items.Length; i++) {
            if (Container.Items[i] == _slot1)
                ind1 = i;
            if (Container.Items[i] == _slot2)
                ind2 = i;
        }
        InventorySlot tempSlot = Container.Items[ind1];
        Container.Items[ind1] = Container.Items[ind2];
        Container.Items[ind2] = tempSlot;
    }

    [ContextMenu("Save")]
    public void Save() {
        //string saveData = JsonUtility.ToJson(this, true);
        //BinaryFormatter bf = new BinaryFormatter();
        //FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        //bf.Serialize(file, saveData);
        //file.Close();

        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, Container);
        stream.Close();
    }

    [ContextMenu("Load")]
    public void Load() {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath))) {
            //BinaryFormatter bf = new BinaryFormatter();
            //FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            //JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            //file.Close();

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
            Container = (Inventory)formatter.Deserialize(stream);
            stream.Close();
        }
    }

    [ContextMenu("Clear")]
    public void Clear() {
        Container = new Inventory();
        for (int i = 0; i < Container.Items.Length; i++) {
            Container.Items[i] = new InventorySlot();
        }
    }
}
