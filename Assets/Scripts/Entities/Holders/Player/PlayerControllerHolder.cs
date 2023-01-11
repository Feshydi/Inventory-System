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
    private RayData _rayData;

    [SerializeField]
    private PlayerControls _inputActions;

    #endregion

    #region Methods

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

        _inputActions = new PlayerControls();
        _inputActions.Player.Enable();
        _inputActions.Player.Interact.performed += Interact_Performed;
        _inputActions.Player.Inventory.performed += Inventory_performed;
    }

    private void OnDisable()
    {
        _inputActions.Player.Interact.performed -= Interact_Performed;
        _inputActions.Player.Inventory.performed -= Inventory_performed;
        _inputActions.Player.Disable();
    }

    private void Update()
    {
        RayHit();
    }

    // interact with smth, based on ray
    private void Interact_Performed(InputAction.CallbackContext context)
    {
        if (_playerData.IsInventoryOpened)
            return;

        if (IsRayHit(out RaycastHit raycastHit))
        {
            var hitObject = raycastHit.transform.gameObject;

            // if ray hits a chest
            if (hitObject.CompareTag("Chest"))
            {
                // open static and dynamic UIs
                hitObject.GetComponent<ChestInventory>().Interact();

                Inventory_performed(context);
            }
            // if ray hits an item
            else if (hitObject.CompareTag("Item"))
            {
                hitObject.GetComponent<GroundItem>().AddItem(GetComponent<Collider>());
            }
            // if ray hits a craft keeper
            else if (hitObject.CompareTag("CraftKeeper"))
            {
                hitObject.GetComponent<CraftKeeper>().Interact();

                Inventory_performed(context);
            }
        }
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

        if (IsRayHit(out RaycastHit raycastHit))
        {
            var hitObject = raycastHit.transform.gameObject;

            // if ray hits an item
            if (hitObject.CompareTag("Item"))
            {
                InventoryController.Instance.InteractText.text = "e to grab";
                InventoryController.Instance.DescriptionText.text = hitObject.GetComponent<GroundItem>().ObjectItem.ToString();
                InventoryController.Instance.SetInteractTextActive(true);
                InventoryController.Instance.SetDescriptionTextActive(true);
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
        InventoryController.Instance.DescriptionText.text = "";
        InventoryController.Instance.SetInteractTextActive(false);
        InventoryController.Instance.SetDescriptionTextActive(false);
    }

    // check ray hit
    // if hit, then out
    private bool IsRayHit(out RaycastHit raycastHit)
    {
        return Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)), out raycastHit, _rayData.RayRange);
    }

    #endregion

}
