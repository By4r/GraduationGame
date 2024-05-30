using DG.Tweening;
using Runtime.Controllers;
using Runtime.Controllers.Security_Room;
using Runtime.Managers;
using Runtime.StateManagers;
using Runtime.TaskStateSystem.TaskUI;
using UnityEngine;

namespace Runtime.TaskStateSystem.TaskStates
{
    public class CheckCameraState : ITaskState
    {
        private TaskInfoManager _taskInfoManager;
        private TaskStateManager _stateManager;
        private CheckCameraManager _checkCameraManager;
        private SecurityRoomController _securityRoomController;
        private bool _isParanormalAppeared;
        
        
        
        public void EnterState(TaskStateManager stateManager)
        {
            Debug.Log("Entering CheckCamera State");

            _checkCameraManager = stateManager.GetCheckCameraManager();
            
            _taskInfoManager = stateManager.GetTaskInfoManager();
            
            _taskInfoManager.SetStateForInfo("CheckCamera");

            _securityRoomController = stateManager.GetSecurityRoomController();
            
            _checkCameraManager.ShowParanormal();
            
            _securityRoomController.SetParanormalUpdateAction(UpdateParanormalStatus);
            
            _securityRoomController.IsCheckCameraState = true;
            
            _stateManager = stateManager;

        }

        public void UpdateState(TaskStateManager stateManager)
        {
            Debug.Log("CheckCamera State");
        }
        
        private void UpdateParanormalStatus(bool activated)
        {
            Debug.Log($"UpdateParanormalStatus called with value: {activated}");
            _isParanormalAppeared = activated;

            if (_isParanormalAppeared)
            {
                // Add a delay of 5 seconds before executing the code
                DOVirtual.DelayedCall(5f, () =>
                {
                    Debug.Log("Exit condition met: transitioning to CheckCameraState");
                    _checkCameraManager.HideParanormal();
                    _stateManager.SetState(new CheckOfficeState());
                });
            }
        }

        
        public void ExitState(TaskStateManager stateManager)
        {
            _taskInfoManager.HideInfoTab();
            Debug.Log("Exiting CheckCamera State");
        }
        
        
    }
}