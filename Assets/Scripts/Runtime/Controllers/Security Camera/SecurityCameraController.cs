using System.Collections.Generic;
using Runtime.Controllers.Camera;
using Runtime.Controllers.Player;
using Runtime.Enums;
using Runtime.Managers;
using Runtime.Signals;
using UnityEngine;


namespace Runtime.Controllers.Security_Camera
{
    public class SecurityCameraController : MonoBehaviour
    {
        #region SelfVariables

        #region Serialized Variables
        [SerializeField] private PlayerMovementController _playerMovementController;
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private float range;
        [SerializeField] private PlayerManager _playerManager;
        [SerializeField] private List<GameObject> cameras;
        [SerializeField] private bool isPauseState;
        #endregion
        
        #region Private Variables
        private int cameraSelected;
        bool isSecurityCamOpen;
        
        #endregion

        #region Public Variables
        
        public bool isSecurityPanelOpen;
        
        #endregion
       
        #endregion

        readonly private string _secCam = "SecurityCamera";
        private void Update()
        {
            Ray theRaycast = new Ray(_playerManager.playerEyes.transform.position, 
                _playerManager.playerEyes.transform.TransformDirection(range * Vector3.forward));
            
            Debug.DrawRay(transform.position,transform.TransformDirection(Vector3.forward * range),Color.green);
            
            if (Physics.Raycast(theRaycast, out RaycastHit hit,range))
            {
                if (hit.collider.CompareTag(_secCam)&& !isPauseState)
                {
                    Debug.Log("sec cam hit raycast");
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
        
    }
    
}