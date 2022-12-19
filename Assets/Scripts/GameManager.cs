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

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                new GameManager();
            return _instance;
        }
    }

    public ItemDatabaseObject Database
    {
        get { return _database; }
    }

    #endregion

    #region Constructors

    private GameManager()
    {
        _instance = this;
    }

    #endregion

}
