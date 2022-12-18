using System.Collections.Generic;
using System.ComponentModel;
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
    public int capacity;
    public InventorySlot[] Items;

    public Inventory() {
        Items = new InventorySlot[capacity];
    }

    public void Init(int size) {
        Items = new InventorySlot[size];
        for (int i = 0; i < Items.Length; i++) {
            Items[i] = new InventorySlot();
        }
    }
}

[CreateAssetMenu(menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject, ISerializationCallbackReceiver {
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

    public InventorySlot GetItem(InventorySlot _slot) {
        for (int i = 0; i < Container.Items.Length; i++) {
            if (Container.Items[i] == _slot)
                return Container.Items[i];
        }
        return null;
    }

    public int GetItemIndex(InventorySlot _slot) {
        for (int i = 0; i < Container.Items.Length; i++) {
            if (Container.Items[i] == _slot)
                return i;
        }
        return -1;
    }

    [ContextMenu("Save")]
    public void Save() {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, Container);
        stream.Close();
    }

    [ContextMenu("Load")]
    public void Load() {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath))) {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
            Container = (Inventory)formatter.Deserialize(stream);
            stream.Close();
        }
    }

    [ContextMenu("Clear")]
    public void Clear() {
        Container.Init(Container.capacity);
    }

    public void OnBeforeSerialize() { }

    public void OnAfterDeserialize() {
        Container.Items = new InventorySlot[Container.capacity];
    }
}
