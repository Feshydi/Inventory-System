using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InventoryHolder : MonoBehaviour
{

    #region Fields

    [SerializeField]
    private string _name;

    [SerializeField]
    protected string _savePath;

    [SerializeField]
    protected int _primaryInventorySize;

    [SerializeField]
    protected InventorySystem _primaryInventorySystem;

    [System.NonSerialized]
    private static UnityAction<InventorySystem> _onDynamicInventoryDisplayRequested;

    #endregion

    #region Properties

    public string Name
    {
        get { return _name; }
    }

    public int PrimaryInvetorySize
    {
        get { return _primaryInventorySize; }
    }

    public InventorySystem PrimaryInventorySystem
    {
        get { return _primaryInventorySystem; }
    }

    public static UnityAction<InventorySystem> OnDynamicInventoryDisplayRequested
    {
        get { return _onDynamicInventoryDisplayRequested; }
        set { _onDynamicInventoryDisplayRequested = value; }
    }

    #endregion

    #region Methods

    protected virtual void Awake()
    {
        Load();

        if (_primaryInventorySystem.InventorySize != _primaryInventorySize)
            Clear();
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    [ContextMenu("Save")]
    protected virtual void Save()
    {
        string path = string.Concat(Application.persistentDataPath, "/", gameObject.name, "_", _savePath);

        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, _primaryInventorySystem);
        stream.Close();
    }

    [ContextMenu("Load")]
    protected virtual void Load()
    {
        string path = string.Concat(Application.persistentDataPath, "/", gameObject.name, "_", _savePath);

        if (File.Exists(path))
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(path), FileMode.Open, FileAccess.Read);
            _primaryInventorySystem = (InventorySystem)formatter.Deserialize(stream);
            stream.Close();
        }
    }

    [ContextMenu("Clear")]
    protected virtual void Clear()
    {
        _primaryInventorySystem = new InventorySystem(_primaryInventorySize);
    }

    #endregion

}
