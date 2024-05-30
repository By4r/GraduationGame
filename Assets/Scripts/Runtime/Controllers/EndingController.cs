using Runtime.Controllers.Camera;
using Runtime.Controllers.Player;
using UnityEngine;

namespace Runtime.Controllers.UI
{
    public class EndingController : MonoBehaviour
    {
        [SerializeField] private bool _isInventoryPanelOpen;
        [SerializeField] private GameObject[] inventoryPanel;
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private PlayerMovementController _playerMovementController;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                InventoryToggle();
                if (_isInventoryPanelOpen)
                {
                    OpenInventoryPanel();
                }
                else
                {
                    CloseAllPanels();
                }
            }

            // if (Input.GetKeyDown(KeyCode.Escape))
            // {
            //     CloseAllPanels();
            // }
        }

        
        
        private void InventoryToggle()
        {
            _isInventoryPanelOpen = !_isInventoryPanelOpen;
        }

        internal void OpenInventoryPanel()
        {
            inventoryPanel[0].SetActive(true);
            _cameraController.mouseState = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            _playerMovementController.canMove = false;
            Debug.Log("Inventory panel opened");
        }

        private void CloseInventoryPanel()
        {
            inventoryPanel[0].SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _playerMovementController.canMove = true;
            _cameraController.mouseState = true;
            Debug.Log("Inventory panel closed");
        }

        public void LetterPanel1()
        {
            inventoryPanel[1].SetActive(true);
        }
        public void LetterPanel2()
        {
            inventoryPanel[2].SetActive(true);
        }
        public void LetterPanel3()
        {
            inventoryPanel[3].SetActive(true);
        }
        private void CloseAllPanels()
        {
            foreach (var panel in inventoryPanel)
            {
                panel.SetActive(false);
            }
            _isInventoryPanelOpen = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _playerMovementController.canMove = true;
            _cameraController.mouseState = true;
            Debug.Log("All panels closed");
        }
    }
}