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

            // Find the PadLockPassword component and subscribe to the event
            _padLockPassword = GameObject.FindObjectOfType<PadLockPassword>();
            if (_padLockPassword != null)
            {
                _padLockPassword.OnPasswordCorrect += SetPasswordTrue;
            }
        }

        public void UpdateState(TaskStateManager stateManager)
        {
            Debug.Log("CHECK OFFICE STATE!");

            Ray raycast = _playerPhysicsController.GetRaycast();
            float range = _playerPhysicsController.range;

            if (_isPasswordTrue)
            {
                Debug.Log("DRAWER OPENED!");
            }
            
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

                if (hit.collider.CompareTag("LockPad"))
                {
                    _lockPadTransform = hit.collider.transform;

                    if (_originalLockPadPosition == Vector3.zero)
                    {
                        _originalLockPadPosition = _lockPadTransform.position;
                        _originalLockPadRotation = _lockPadTransform.rotation;
                    }

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Debug.Log("1111111111111111");
                        _playerMovementController.canMove = false;
                        Vector3 spawnPosition = _playerPhysicsController.transform.position + _playerPhysicsController.transform.forward * 2f;
                        _lockPadTransform.position = spawnPosition;
                        _lockPadTransform.rotation = _playerPhysicsController.transform.rotation;
                    }

                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        _playerMovementController.canMove = true;
                        _lockPadTransform.position = _originalLockPadPosition;
                        _lockPadTransform.rotation = _originalLockPadRotation;
                    }
                }
            }
        }

        public void ExitState(TaskStateManager stateManager)
        {
            _taskInfoManager.HideInfoTab();
            Debug.Log("Exiting CheckOffice State");

            // Unsubscribe from the event
            if (_padLockPassword != null)
            {
                _padLockPassword.OnPasswordCorrect -= SetPasswordTrue;
            }
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
