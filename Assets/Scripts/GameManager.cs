using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    #region Fields

    private static GameManager _instance;

    [SerializeField]
    private ItemDatabaseObject _database;

    #endregion

    #region Properties

    public static GameManager Instance => _instance == null ? new GameManager() : _instance;

    public ItemDatabaseObject Database
    {
        get { return _database; }
    }

    #endregion

    #region Constructors

    private GameManager() => _instance = this;

    #endregion

    #region Methods

    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
            Cursor.lockState = CursorLockMode.Locked;
    }

    #endregion

}
