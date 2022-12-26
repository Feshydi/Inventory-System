using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class GroundItem : MonoBehaviour
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

    public ItemObject ObjectItem
    {
        get { return _itemObject; }
    }

    public int StackSize
    {
        get { return _stackSize; }
    }

    #endregion

    #region Methods

    private void Awake()
    {
        _sphereCollider = GetComponent<SphereCollider>();
        _sphereCollider.isTrigger = true;
        _sphereCollider.radius = _pickUpRadius;
    }

    private void OnTriggerEnter(Collider other)
    {
        var inventory = other.transform.GetComponent<InventoryHolder>();

        if (!inventory)
            return;

        if (inventory.InventorySystem.AddToInventory(_itemObject, _stackSize, out int amountLeft))
            Destroy(gameObject);
        else
            _stackSize = amountLeft;
    }

    #endregion

}
