using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerHolder : MonoBehaviour
{

    #region Fields

    [Header("Static Data")]
    [SerializeField]
    private PlayerData _playerData;

    [SerializeField]
    private PlayerControls _inputActions;

    [SerializeField]
    private RayHolder _rayHolder;

    #endregion

    #region Methods

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

        _inputActions = new PlayerControls();
        _inputActions.Player.Enable();
        _inputActions.Player.Inventory.performed += Inventory_performed;
    }

    private void OnDisable()
    {
        _inputActions.Player.Inventory.performed -= Inventory_performed;
        _inputActions.Player.Disable();
    }

    private void Update()
    {
        RayHit();
    }

    // open/close static and close only dynamic inventories
    private void Inventory_performed(InputAction.CallbackContext context)
    {
        // set cursor
        Cursor.lockState = _playerData.IsInventoryOpened ? CursorLockMode.Locked : CursorLockMode.None;
        // need to fix screen flick after locked

        _playerData.IsInventoryOpened = !_playerData.IsInventoryOpened;

        // close static UI
        InventoryController.Instance.SetStaticInventoryActive(_playerData.IsInventoryOpened);

        // if close static UI, then close other
        if (!_playerData.IsInventoryOpened)
        {
            InventoryController.Instance.SetDynamicInventoryActive(false);
            InventoryController.Instance.SetCraftingActive(false);
        }
    }

    // update ray check for everything
    private void RayHit()
    {
        if (_playerData.IsInventoryOpened)
            return;

        if (_rayHolder.CastRay(out RaycastHit raycastHit))
        {
            var hitObject = raycastHit.transform.gameObject;

            // if ray hits an item
            if (hitObject.CompareTag("Item"))
            {
                InventoryController.Instance.InteractText.text = "e to grab";
                InventoryController.Instance.SetInteractTextActive(true);

                BaseItemActions.OnPointedItem.Invoke(hitObject.GetComponent<GroundItem>().ObjectItem.ToString(), true);
            }
            // if not compare, set default everything
            else
                RayHitDefault();
        }
        // if not hits, set default everything
        else
            RayHitDefault();
    }

    private void RayHitDefault()
    {
        InventoryController.Instance.InteractText.text = "";
        InventoryController.Instance.SetInteractTextActive(false);

        BaseItemActions.OnPointedItem.Invoke("", false);
    }

    #endregion

}
