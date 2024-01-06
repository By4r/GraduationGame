using System;
using System.Collections;
using System.Collections.Generic;
using Runtime.Controllers.Camera;
using Runtime.Controllers.Player;
using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Runtime.Controllers.Security_Camera
{
    public class SecurityCameraController : MonoBehaviour
    {
        public List<GameObject> cameras;
        private int cameraSelected;
        [SerializeField] private PlayerMovementController _playerMovementController;
        [SerializeField] private CameraController _cameraController;
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
                ZoomIn();
            }
            if (Input.GetKeyDown(KeyCode.Y))
            {
                ZoomOut();
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
        
        private void ZoomIn()
        {
            CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.SecurityCamera, 2);
            _playerMovementController.canMove=false;
            _cameraController.mouseState = false;

        }

        private void ZoomOut()
        {
            _playerMovementController.canMove=true;
            CoreUISignals.Instance.onClosePanel?.Invoke(2);
            _cameraController.mouseState = true;
        }
    }
}