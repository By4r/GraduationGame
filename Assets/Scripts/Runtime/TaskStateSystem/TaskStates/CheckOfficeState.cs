using Runtime.Controllers.Camera;
using Runtime.Controllers.Player;
using Runtime.Controllers.UI;
using Runtime.TaskStateSystem.TaskUI;
using Runtime.TaskSystem;
using UnityEngine;

namespace Runtime.TaskStateSystem.TaskStates
{
    public class CheckOfficeState : ITaskState
    {
        private PlayerPhysicsController _playerPhysicsController;
        private TaskInfoManager _taskInfoManager;
        private PlayerMovementController _playerMovementController;
        private PadLockPassword _padLockPassword;
        private CameraController _cameraController;
        private CheckOfficeManager _checkOfficeManager;
        
        private bool _isPasswordTrue;
        private Vector3 _originalLockPadPosition;
        private Quaternion _originalLockPadRotation;
        private Transform _lockPadTransform;
        
        private bool _keyReceived = false;

        public void EnterState(TaskStateManager stateManager)
        {
            Debug.Log("Entering CheckOffice State");
            _playerPhysicsController = stateManager.GetPlayerPhysicsController();
            _playerMovementController = stateManager.GetPlayerMovementController();
            _checkOfficeManager = stateManager.GetCheckOfficeManager();

          
            _taskInfoManager = stateManager.GetTaskInfoManager();

            _taskInfoManager.SetStateForInfo("CheckOffice");
        }

        public void UpdateState(TaskStateManager stateManager)
        {
            Debug.Log("CHECK OFFICE STATE!");

            Ray raycast = _playerPhysicsController.GetRaycast();
            float range = _playerPhysicsController.range;

            if (Physics.Raycast(raycast, out RaycastHit hit, range))
            {

                if (hit.collider.CompareTag("Drawer"))
                {
                    if (Input.GetKey(KeyCode.E))
                    {
                        _checkOfficeManager.PlayDrawerAnimation();
                    }
                }
                
                if (hit.collider.CompareTag("Letter"))
                {
                    
                    if (Input.GetKey(KeyCode.E))
                    {
                        LetterReceived(stateManager);
                        _keyReceived = true; 
                        _checkOfficeManager.atticNotePanel.SetActive(true);
                    }
                }
                
                // if (hit.collider.CompareTag("Letter") && _checkOfficeManager.DrawerAnimationPlayed && !_keyReceived)
                // {
                //     if (Input.GetKey(KeyCode.E))
                //     {
                //         LetterReceived(stateManager);
                //         _keyReceived = true; 
                //     }
                // }
            }
            if (_keyReceived)
            {
                _checkOfficeManager.atticNotePanel.SetActive(true);
            }
            
        }

        public void ExitState(TaskStateManager stateManager)
        {
            _taskInfoManager.HideInfoTab();
            Debug.Log("Exiting CheckOffice State");
        }

        private void LetterReceived(TaskStateManager taskStateManager)
        {
            Debug.Log("Letter Received !");
           
            taskStateManager.SetState(new AtticState());
        }

        private void SetPasswordTrue()
        {
            _isPasswordTrue = true;
            Debug.Log("Password is correct, _isPasswordTrue set to true");
        }
    }
}