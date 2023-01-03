using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class PlayerInventory : InventoryHolder
{

    #region Fields

    [SerializeField]
    protected int _backpackInventorySize;

    [SerializeField]
    protected InventorySystem _backpackInventorySystem;

    [SerializeField]
    protected int _equipmentInventorySize;

    [SerializeField]
    protected InventorySystem _equipmentInventorySystem;

    #endregion

    #region Properties

    public int BackpackInvetorySize
    {
        get { return _backpackInventorySize; }
    }

    public InventorySystem BackpackInventorySystem
    {
        get { return _backpackInventorySystem; }
    }

    public int EquipmentInventorySize
    {
        get { return _equipmentInventorySize; }
    }

    public InventorySystem EquipmentInventorySystem
    {
        get { return _equipmentInventorySystem; }
    }

    #endregion

    #region Methods

    protected override void Awake()
    {
        Load();

        if (_primaryInventorySystem.InventorySize != _primaryInventorySize ||
            _backpackInventorySystem.InventorySize != _backpackInventorySize ||
            _equipmentInventorySystem.InventorySize != _equipmentInventorySize)
            Clear();
    }

    [ContextMenu("Save")]
    protected override void Save()
    {
        string path = string.Concat(Application.persistentDataPath, "/", gameObject.name, "_", _savePath);
        List<InventorySystem> inventorySystems = new List<InventorySystem>();
        inventorySystems.Add(_primaryInventorySystem);
        inventorySystems.Add(_backpackInventorySystem);
        inventorySystems.Add(_equipmentInventorySystem);

        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, inventorySystems);
        stream.Close();
    }

    [ContextMenu("Load")]
    protected override void Load()
    {
        string path = string.Concat(Application.persistentDataPath, "/", gameObject.name, "_", _savePath);

        if (File.Exists(path))
        {
            List<InventorySystem> inventorySystems = new List<InventorySystem>();

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(path), FileMode.Open, FileAccess.Read);
            inventorySystems = (List<InventorySystem>)formatter.Deserialize(stream);
            stream.Close();

            _primaryInventorySystem = inventorySystems[0];
            _backpackInventorySystem = inventorySystems[1];
            _equipmentInventorySystem = inventorySystems[2];
        }
    }

    protected override void Clear()
    {
        base.Clear();

        _backpackInventorySystem = new InventorySystem(_backpackInventorySize);
        _equipmentInventorySystem = new InventorySystem(_equipmentInventorySize);
    }

    #endregion

}
