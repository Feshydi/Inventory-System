using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using static UnityEditor.Progress;

[System.Serializable]
public class InventorySlot {
    public int ID;
    public Item item;
    [Range(1, 99)]
    public int amount = 1;
    private int maxStackAmount;

    public InventorySlot(int _id, Item _item, int _amount, int _maxStackAmount) {
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
    public List<InventorySlot> Items;

    public Inventory() {
        Items = new List<InventorySlot>(capacity);
    }

    public bool IsFull() {
        return Items.Count >= Items.Capacity;
    }
}

[CreateAssetMenu(menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject {
    public string savePath;
    public ItemDatabaseObject database;
    public Inventory Container = new Inventory();

    public void AddItem(Item _item, int _amount) {
        if (!AddItemToStack(_item, _amount))
            Container.Items.Add(new InventorySlot(_item.Id, _item, _amount, _item.MaxStack));
    }

    public bool AddItemToStack(Item _item, int _amount) {
        foreach (var slot in Container.Items) {
            if (slot.item.Id == _item.Id && slot.amount < slot.item.MaxStack) {
                slot.AddAmount(_amount);
                return true;
            }
        }
        return false;
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
    }
}