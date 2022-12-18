using UnityEngine;

public class PlayerController : MonoBehaviour
{

    #region Fields

    [SerializeField]
    private string _name;

    #endregion

    #region Properties

    public string Name
    {
        get { return _name; }
    }

    #endregion

    public GameObject inventoryWindow;
    public InventoryObject inventory;
    public InventoryObject equipment;

    private void Start()
    {
        LoadInvetory();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ShowOrHideInventoryWindow();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //var item = other.GetComponent<GroundItem>();
        //if (item && !inventory.IsFull(new Item(item.item)))
        //{
        //    inventory.AddItem(new Item(item.item), 1);
        //    Destroy(other.gameObject);
        //}
    }

    private void OnApplicationQuit()
    {
        ClearInventory();
    }

    public void SaveInventory()
    {
        inventory.Save();
        equipment.Save();
    }

    public void LoadInvetory()
    {
        inventory.Load();
        equipment.Load();
        foreach (var inventoryManager in inventoryWindow.GetComponentsInChildren<InventoryManager>())
        {
            inventoryManager.CreateSlots();
        }
    }

    public void ClearInventory()
    {
        inventory.Clear();
        equipment.Clear();
        foreach (var inventoryManager in inventoryWindow.GetComponentsInChildren<InventoryManager>())
        {
            inventoryManager.CreateSlots();
        }
    }

    public void ShowOrHideInventoryWindow()
    {
        inventoryWindow.SetActive(!inventoryWindow.activeSelf);
    }

    #region Methods

    public override string ToString()
    {
        return Name.ToString();
    }

    #endregion

}
