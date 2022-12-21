using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class GroundItem : MonoBehaviour
{

    #region Fields

    private SphereCollider _sphereCollider;

    [SerializeField]
    private ItemObject _itemObject;

    [SerializeField]
    private int _stackSize;

    [SerializeField]
    private float _pickUpRadius = 1f;

    #endregion

    #region Properties

    public SphereCollider SphereCollider
    {
        get { return _sphereCollider; }
    }

    public ItemObject ObjectItem
    {
        get { return _itemObject; }
    }

    public int StackSize
    {
        get { return _stackSize; }
    }

    public float PickUpRadius
    {
        get { return _pickUpRadius; }
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
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            _stackSize = amountLeft;
        }
    }

    #endregion

}
