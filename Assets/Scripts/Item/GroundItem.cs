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
        AddItem(other);
    }

    private void OnTriggerStay(Collider other)
    {
        AddItem(other);
    }

    private void AddItem(Collider other)
    {
        var inventory = other.transform.GetComponent<PlayerInventory>();

        if (!inventory)
            return;

        int amountLeft;
        if (inventory.PrimaryInventorySystem.AddToInventory(_itemObject, _stackSize, out amountLeft))
        {
            Destroy(gameObject);
            return;
        }
        if (inventory.BackpackInventorySystem.AddToInventory(_itemObject, amountLeft, out amountLeft))
        {
            Destroy(gameObject);
            return;
        }
        _stackSize = amountLeft;
    }

    #endregion

}
