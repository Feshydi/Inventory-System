using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Database", menuName = "Inventory System/Database")]
public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{

    #region Fields

    [SerializeField]
    private ItemObject[] _items;

    [SerializeField]
    private Dictionary<int, ItemObject> _getItem = new Dictionary<int, ItemObject>();

    #endregion

    #region Properties

    public Dictionary<int, ItemObject> GetItem
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
        _getItem = new Dictionary<int, ItemObject>();
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
