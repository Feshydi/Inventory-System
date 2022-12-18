using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Database", menuName = "Inventory System/Inventory Items/Database")]
public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{

    #region Fields

    [SerializeField]
    private ObjectItem[] _items;

    [SerializeField]
    private Dictionary<int, ObjectItem> _getItem = new Dictionary<int, ObjectItem>();

    #endregion

    #region Properties

    public ObjectItem[] Items
    {
        get { return _items; }
    }

    public Dictionary<int, ObjectItem> GetItem
    {
        get { return _getItem; }
    }

    #endregion

    #region Methods

    private void SetData()
    {
        for (int i = 0; i < _items.Length; i++)
        {
            _items[i].ID = i;
            _getItem.Add(i, _items[i]);
        }
    }

    private void ClearData()
    {
        _getItem = new Dictionary<int, ObjectItem>();
    }

    public void OnAfterDeserialize()
    {
        SetData();
    }

    public void OnBeforeSerialize()
    {
        ClearData();
    }

    #endregion

}
