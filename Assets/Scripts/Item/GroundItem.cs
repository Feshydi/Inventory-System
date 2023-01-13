using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class GroundItem : MonoBehaviour, IInteractable
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

    public void Interact()
    {
        //int amountLeft;

        //var hotbar = other.GetComponent<PlayerHotbar>();
        //if (!hotbar) return;
        //if (hotbar.InventorySystem.AddToInventory(_itemObject, _stackSize, out amountLeft))
        //{
        //    Destroy(gameObject);
        //    return;
        //}

        //var inventory = other.GetComponent<PlayerInventory>();
        //if (!inventory) return;
        //if (inventory.InventorySystem.AddToInventory(_itemObject, amountLeft, out amountLeft))
        //{
        //    Destroy(gameObject);
        //    return;
        //}

        //var backpack = other.GetComponent<PlayerBackpack>();
        //if (!backpack) return;
        //if (backpack.InventorySystem.AddToInventory(_itemObject, amountLeft, out amountLeft))
        //{
        //    Destroy(gameObject);
        //    return;
        //}
        //_stackSize = amountLeft;
    }

    public void Interact(PlayerInventoryController player)
    {
        switch (_itemObject.GetType().Name)
        {
            case var value when value == typeof(BootsType).Name:
            case var value1 when value1 == typeof(ChestType).Name:
            case var value2 when value2 == typeof(HelmetType).Name:
                if (player.TryGetComponent(out PlayerWeaponInventoryHolder holder))
                    if (holder.InventorySystem.AddToInventory(_itemObject, _stackSize, out _stackSize))
                        Destroy(gameObject);
                return;
        }
    }

    public override string ToString()
    {
        return _itemObject.ToString();
    }

    #endregion

}
