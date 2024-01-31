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
        #region SelfVariables

        #region Serialized Variables
        [SerializeField] private PlayerMovementController _playerMovementController;
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private CapturePhotoController _capturePhotoController;
        [SerializeField] private float range;
        [SerializeField] private PlayerManager _playerManager;
        [SerializeField] private List<GameObject> cameras;
        [SerializeField] private bool isPauseState;
        [SerializeField] private GameObject _secCameraPressEtext;
        [SerializeField] private GameObject _infoPaperPressEtext;
        #endregion
        
        #region Private Variables
        private int cameraSelected;
        private bool isSecurityCamOpen;
        private bool isInfoPaperPanelOpen;
        
        #endregion

        #region Public Variables
        
        public bool isSecurityPanelOpen;
        
        #endregion
       
        #endregion

        private readonly string _secCam = "SecurityCamera";
        private readonly string _infoPaper = "InfoPaper";
        
        private void Update()
        {
            Ray theRaycast = new Ray(_playerManager.playerEyes.transform.position, 
                _playerManager.playerEyes.transform.TransformDirection(range * Vector3.forward));
            
            Debug.DrawRay(transform.position,transform.TransformDirection(Vector3.forward * range),Color.green);
            
            if (Physics.Raycast(theRaycast, out RaycastHit hit,range))
            {
                
                if (hit.collider.CompareTag(_secCam)&& !isPauseState )
                {
                    _secCameraPressEtext.SetActive(true);
                    
                    if (Input.GetKeyDown(KeyCode.D))
                    {
                        NextCam();
                    }

                    if (Input.GetKeyDown(KeyCode.A))
                    {
                        PreviousCam();
                    }
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
                }
                if (hit.collider.CompareTag(_infoPaper) && !isPauseState)
                {   
                    _infoPaperPressEtext.SetActive(true);
                    
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if (!isInfoPaperPanelOpen)
                        {
                            InfoPaperPanelOpen();
                        }
                        else
                        {
                            InfoPaperPanelClose();
                        }
                    }
               
                }
            }
            else
            {
                _secCameraPressEtext.SetActive(false);
                _infoPaperPressEtext.SetActive(false);
            }
            

           
        }
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void OnPauseState(bool state)
        {
            isPauseState = state;
        }
        private void SubscribeEvents()
        {
            
            PauseSignals.Instance.onPauseState += OnPauseState;
        }
        private void UnSubscribeEvents()
        {
            PauseSignals.Instance.onPauseState -= OnPauseState;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void Start()
        {
            _capturePhotoController = FindObjectOfType<CapturePhotoController>();
        }

        private void InfoPaperPanelOpen()
        {
            CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.PaperInfo, 4);
            isInfoPaperPanelOpen = true;
            _playerMovementController.canMove=false;
            _cameraController.mouseState = false;
            _capturePhotoController.isPhotoPanelOpen = false;
        }
        private void InfoPaperPanelClose()
        {
            CoreUISignals.Instance.onClosePanel?.Invoke(4);
            isInfoPaperPanelOpen = false;
            _playerMovementController.canMove=true;
            _cameraController.mouseState = true;
        }
//  e basınca ekranın önüne 
        private void NextCam()
        {
            cameras[cameraSelected].SetActive(false); 

            cameraSelected = (cameraSelected + 1) % cameras.Count; 

            cameras[cameraSelected].SetActive(true); 
            //Debug.Log("Selected Camera: " + cameraSelected);
        }

        private void PreviousCam()
        {
            cameras[cameraSelected].SetActive(false); 

            cameraSelected = (cameraSelected - 1 + cameras.Count) % cameras.Count;

            cameras[cameraSelected].SetActive(true); 
           // Debug.Log("Selected Camera: " + cameraSelected);
        }
        
        private void SecurityCamOpen()
        {
            Debug.Log("SecurityCam OPENED");
            CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.SecurityCamera, 4);
            isSecurityPanelOpen = true;
            _playerMovementController.canMove=false;
            _cameraController.mouseState = false;
            
        }

        private void SecurityCamClose()
        {
            CoreUISignals.Instance.onClosePanel?.Invoke(4);
            isSecurityPanelOpen = false;
            _playerMovementController.canMove=true;
            _cameraController.mouseState = true;
        }
        
    }
    
}