using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class ItemHolder : MonoBehaviour, IInteractable
{

    #region Fields

    [SerializeField]
    private SphereCollider _sphereCollider;

    [SerializeField]
    private ItemObject _itemObject;

    [SerializeField]
    private int _stackSize;

    [SerializeField]
    private float _pickUpRadius = 0.5f;

    #endregion

    #region Properties

    #endregion

    #region Methods

    private void Awake()
    {
        _sphereCollider = GetComponent<SphereCollider>();
        _sphereCollider.isTrigger = true;
        _sphereCollider.radius = _pickUpRadius;
    }

    public void Interact(PlayerInventoryController player)
    {
        var inventoryHolder = _itemObject.GetInventoryType(player);

        if (inventoryHolder != null && inventoryHolder.InventorySystem.AddToInventory(_itemObject, _stackSize, out _stackSize))
            Destroy(gameObject);
    }

    public override string ToString()
    {
        return string.Concat(
            _itemObject.Title, "\n",
            _itemObject.ToString()
            );
    }

    #endregion

}
