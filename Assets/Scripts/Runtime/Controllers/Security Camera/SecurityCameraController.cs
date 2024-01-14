using System.Collections.Generic;
using Runtime.Controllers.Camera;
using Runtime.Controllers.Player;
using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;


namespace Runtime.Controllers.Security_Camera
{
    public class SecurityCameraController : MonoBehaviour
    {
        public List<GameObject> cameras;
        private int cameraSelected;
        [SerializeField] private PlayerMovementController _playerMovementController;
        [SerializeField] private CameraController _cameraController;
        public bool isSecurityPanelOpen;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                NextCam();
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                PreviousCam();
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                SecurityCamOpen();
            }
            if (Input.GetKeyDown(KeyCode.Y))
            {
                SecurityCamClose();
            }
        }
//  e basınca ekranın önüne 
        private void NextCam()
        {
            cameras[cameraSelected].SetActive(false); 

            cameraSelected = (cameraSelected + 1) % cameras.Count; 

            cameras[cameraSelected].SetActive(true); 
            Debug.Log("Selected Camera: " + cameraSelected);
        }

        private void PreviousCam()
        {
            cameras[cameraSelected].SetActive(false); 

            cameraSelected = (cameraSelected - 1 + cameras.Count) % cameras.Count;

            cameras[cameraSelected].SetActive(true); 
            Debug.Log("Selected Camera: " + cameraSelected);
        }
        
        private void SecurityCamOpen()
        {
            CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.SecurityCamera, 2);
            isSecurityPanelOpen = true;
            _playerMovementController.canMove=false;
            _cameraController.mouseState = false;
        }

        private void SecurityCamClose()
        {
            CoreUISignals.Instance.onClosePanel?.Invoke(2);
            isSecurityPanelOpen = false;
            _playerMovementController.canMove=true;
            _cameraController.mouseState = true;
        }
        
        private void SubscribeEvents()
        {
            SecurityCameraSignals.Instance.onNextCamera += NextCam;
            SecurityCameraSignals.Instance.onPreviousCamera += PreviousCam;
            SecurityCameraSignals.Instance.onSecurityCameraOpen += SecurityCamOpen;
            SecurityCameraSignals.Instance.onSecurityCameraClose += SecurityCamClose;
        }
        private void UnSubscribeEvents()
        {
            SecurityCameraSignals.Instance.onNextCamera -= NextCam;
            SecurityCameraSignals.Instance.onPreviousCamera -= PreviousCam;
            SecurityCameraSignals.Instance.onSecurityCameraOpen -= SecurityCamOpen;
            SecurityCameraSignals.Instance.onSecurityCameraClose -= SecurityCamClose;
        }
        private void OnDisable()
        {
            UnSubscribeEvents();
        }
        private void OnEnable()
        {
            SubscribeEvents();
        }
    }
    
}