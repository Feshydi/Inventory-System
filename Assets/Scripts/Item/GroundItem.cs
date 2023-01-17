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
        switch (_itemObject)
        {
            case var value when value.GetType().IsSubclassOf(typeof(Sword)):
                if (player.TryGetComponent(out WeaponInventoryHolder weaponHolder))
                    if (weaponHolder.InventorySystem.AddToInventory(_itemObject, _stackSize, out _stackSize))
                        Destroy(gameObject);
                return;
            //case var value when value.GetType().IsSubclassOf(typeof(BowAndArrow)):
            //    if (player.TryGetComponent(out BowAndArrowInventoryHolder bowAndArrowHolder))
            //        if (bowAndArrowHolder.InventorySystem.AddToInventory(_itemObject, _stackSize, out _stackSize))
            //            Destroy(gameObject);
            //    return;
            //case var value when value.GetType().IsSubclassOf(typeof(Shield)):
            //    if (player.TryGetComponent(out ShieldInventoryHolder shieldHolder))
            //        if (shieldHolder.InventorySystem.AddToInventory(_itemObject, _stackSize, out _stackSize))
            //            Destroy(gameObject);
            //    return;
            case var value when value.GetType().IsSubclassOf(typeof(Equipment)):
                if (player.TryGetComponent(out ArmorInventoryHolder armorHolder))
                    if (armorHolder.InventorySystem.AddToInventory(_itemObject, _stackSize, out _stackSize))
                        Destroy(gameObject);
                return;
            //case var value when value.GetType().IsSubclassOf(typeof(Material)):
            //    if (player.TryGetComponent(out MaterialInventoryHolder materialHolder))
            //        if (materialHolder.InventorySystem.AddToInventory(_itemObject, _stackSize, out _stackSize))
            //            Destroy(gameObject);
            //    return;
            //case var value when value.GetType().IsSubclassOf(typeof(Food)):
            //case var value1 when value1.GetType().IsSubclassOf(typeof(Potion)):
            //    if (player.TryGetComponent(out FoodInventoryHolder foodHolder))
            //        if (foodHolder.InventorySystem.AddToInventory(_itemObject, _stackSize, out _stackSize))
            //            Destroy(gameObject);
            //    return;
            case var value when value.GetType().IsSubclassOf(typeof(Default)):
                if (player.TryGetComponent(out KeyItemsInventoryHolder keyItemsHolder))
                    if (keyItemsHolder.InventorySystem.AddToInventory(_itemObject, _stackSize, out _stackSize))
                        Destroy(gameObject);
                return;
        }
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
