using System;
using System.Collections.Generic;
using Runtime.Controllers.Camera;
using Runtime.Controllers.Player;
using Runtime.Enums;
using Runtime.Managers;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.Security_Room
{
    public class SecurityRoomController : MonoBehaviour
    {
        [SerializeField] private PlayerMovementController playerMovementController;
        [SerializeField] private CameraController cameraController;
        [SerializeField] private float range;
        [SerializeField] private PlayerPhysicsController playerPhysicsController;
        [SerializeField] private List<GameObject> cameras;
        [SerializeField] private GameObject secCameraPressEtext;
        [SerializeField] private GameObject secPanel;

        private int cameraSelected;
        private bool isSecurityCamOpen;
        private bool isInfoPaperPanelOpen;
        private bool hasPlayedMonitorSound;
        private bool hasPlayedPaperSound;
        private bool wasCamOfficeActive;

        private readonly string securityCameraTag = "SecurityCamera";
        private readonly string infoPaperTag = "InfoPaper";

        #region Public Variables
        
        public bool isSecurityPanelOpen;
        
        private bool _isCheckCameraState;
        public bool IsCheckCameraState
        {
            get => _isCheckCameraState;
            set => _isCheckCameraState = value;
        }
        
        #endregion
        
        private Action<bool> _updateParanormalStatus;
        
        private void Update()
        {
            Ray raycast = new Ray(playerPhysicsController.playerEyes.transform.position,
                playerPhysicsController.playerEyes.transform.TransformDirection(Vector3.forward * range));

            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward * range), Color.green);

            if (Physics.Raycast(raycast, out RaycastHit hit, range))
            {
                if (hit.collider.CompareTag(securityCameraTag))
                {
                    secCameraPressEtext.SetActive(true);

                    if (Input.GetKeyDown(KeyCode.D))
                        NextCam();

                    if (Input.GetKeyDown(KeyCode.A))
                        PreviousCam();

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if (!isSecurityCamOpen)
                        {
                            SecurityCamOpen();
                            isSecurityCamOpen = true;
                        }
                        else
                        {
                            SecurityCamClose();
                            isSecurityCamOpen = false;
                        }
                    }
                    
                    // Check if the current camera is "CamOffice"
                    if (cameras[cameraSelected].name == "CamOffice" && IsCheckCameraState)
                    {
                        if (!wasCamOfficeActive)
                        {
                            wasCamOfficeActive = true;
                            _updateParanormalStatus?.Invoke(true);
                            Debug.Log("CamOffice is the currently active camera.");
                        }
                    }
                    else
                    {
                        wasCamOfficeActive = false;
                    }
                }
                if (hit.collider.CompareTag(infoPaperTag))
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if (!isInfoPaperPanelOpen)
                            InfoPaperPanelOpen();
                        else
                            InfoPaperPanelClose();
                    }
                }
            }
            else
            {
                secCameraPressEtext.SetActive(false);
            }
        }

        private void InfoPaperPanelOpen()
        {
            CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.PaperInfo, 4);
            isInfoPaperPanelOpen = true;
            playerMovementController.canMove = false;
            cameraController.mouseState = false;
            if (!hasPlayedPaperSound)
            {
                hasPlayedPaperSound = true;
            }
        }

        private void InfoPaperPanelClose()
        {
            CoreUISignals.Instance.onClosePanel?.Invoke(4);
            isInfoPaperPanelOpen = false;
            playerMovementController.canMove = true;
            cameraController.mouseState = true;
            hasPlayedPaperSound = false;
        }

        private void NextCam()
        {
            cameras[cameraSelected].SetActive(false);
            cameraSelected = (cameraSelected + 1) % cameras.Count;
            cameras[cameraSelected].SetActive(true);
        }

        private void PreviousCam()
        {
            cameras[cameraSelected].SetActive(false);
            cameraSelected = (cameraSelected - 1 + cameras.Count) % cameras.Count;
            cameras[cameraSelected].SetActive(true);
        }

        private void SecurityCamOpen()
        {
            Debug.Log("SecurityCam OPENED");
            CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.SecurityCamera, 4);
            isSecurityPanelOpen = true;
            playerMovementController.canMove = false;
            cameraController.mouseState = false;
            secPanel.SetActive(true);
            if (!hasPlayedMonitorSound)
            {
                hasPlayedMonitorSound = true;
            }
        }

        private void SecurityCamClose()
        {
            CoreUISignals.Instance.onClosePanel?.Invoke(4);
            isSecurityPanelOpen = false;
            playerMovementController.canMove = true;
            cameraController.mouseState = true;
            hasPlayedMonitorSound = false;
            secPanel.SetActive(false);
        }
        
        internal void SetParanormalUpdateAction(Action<bool> action)
        {
            _updateParanormalStatus = action;
        }
    }
}
