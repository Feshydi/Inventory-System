using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    #region Singleton

    private static GameManager _instance;

    public static GameManager Instance => _instance == null ? new GameManager() : _instance;

    private GameManager() => _instance = this;

    #endregion

    #region Fields

    [SerializeField]
    private ItemDatabaseObject _database;

    [SerializeField]
    private MouseItem _mouseItem;

    #endregion

    #region Properties

    public ItemDatabaseObject Database => _database;

    public MouseItem MouseItem => _mouseItem;

    #endregion

    #region Methods

    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
            Cursor.lockState = CursorLockMode.Locked;
    }

    #endregion

}
