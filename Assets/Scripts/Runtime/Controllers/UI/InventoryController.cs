using Runtime.Controllers.Camera;
using Runtime.Controllers.Player;
using UnityEngine;

namespace Runtime.Controllers.UI
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] private bool _isInventoryPanelOpen;
        [SerializeField] private GameObject inventoryPanel;
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private PlayerMovementController _playerMovementController;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                InventoryToggle();
               
            }
            if (_isInventoryPanelOpen)
            {
                OpenInventoryPanel();
            }
            else
            {
                CloseInventoryPanel();
            }
        }

        private void InventoryToggle()
        {
            _isInventoryPanelOpen = !_isInventoryPanelOpen;
        }

        private void OpenInventoryPanel()
        {
            inventoryPanel.SetActive(true);
            _cameraController.mouseState = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            _playerMovementController.canMove = false;
            Debug.Log("Inventory panel opened");
        }

        private void CloseInventoryPanel()
        {
            inventoryPanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _playerMovementController.canMove = true;
            _cameraController.mouseState = true;
            Debug.Log("Inventory panel closed");
        }
    }
}