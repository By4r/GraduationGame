using Runtime.Controllers.Camera;
using Runtime.Controllers.Player;
using Runtime.TaskStateSystem.TaskUI;
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
        
        private bool _isPasswordTrue;
        private Vector3 _originalLockPadPosition;
        private Quaternion _originalLockPadRotation;
        private Transform _lockPadTransform;

        public void EnterState(TaskStateManager stateManager)
        {
            Debug.Log("Entering CheckOffice State");
            _playerPhysicsController = stateManager.GetPlayerPhysicsController();
            _playerMovementController = stateManager.GetPlayerMovementController();

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
                if (hit.collider.CompareTag("Key"))
                {
                    if (_isPasswordTrue)
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            KeyReceived(stateManager);
                        }
                    }
                }
                
            }
        }

        public void ExitState(TaskStateManager stateManager)
        {
            _taskInfoManager.HideInfoTab();
            Debug.Log("Exiting CheckOffice State");

        }

        private void KeyReceived(TaskStateManager taskStateManager)
        {
            Debug.Log("Key Received !");
            
            taskStateManager.SetState(new AtticState());
        }
        
        private void SetPasswordTrue()
        {
            _isPasswordTrue = true;
            Debug.Log("Password is correct, _isPasswordTrue set to true");
        }
    }
}
