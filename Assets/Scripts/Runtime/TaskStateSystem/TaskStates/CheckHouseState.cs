using System;
using Runtime.Controllers.Player;
using Runtime.TaskStateSystem.TaskUI;
using Runtime.TaskSystem;
using UnityEngine;

namespace Runtime.TaskStateSystem.TaskStates
{
    public class CheckHouseState : ITaskState
    {
        private TaskInfoManager _taskInfoManager;
        private CheckHouseManager _checkHouseManager;
        private PlayerPhysicsController _playerPhysicsController;
        private bool _paranormalTriggerActivated = false;
        
        public void EnterState(TaskStateManager stateManager)
        {
            Debug.Log("Entering CheckHouse State");

            _playerPhysicsController = stateManager.GetPlayerPhysicsController();
            
            _playerPhysicsController.SetParanormalTriggerStatusUpdateAction(UpdateParanormalTriggerStatus);
            
            _checkHouseManager = stateManager.GetCheckHouseManager();
            
            _taskInfoManager = stateManager.GetTaskInfoManager();
            
            _taskInfoManager.SetStateForInfo("CheckHouse");
            
            _checkHouseManager.ActiveTriggers();
        }

        public void UpdateState(TaskStateManager stateManager)
        {
            Debug.Log("CheckHouse State");
            
            if (_paranormalTriggerActivated)
            {
                _checkHouseManager.ShowParanormal();
            }
            else if(_paranormalTriggerActivated!)
            {
                _checkHouseManager.HideParanormal();
            }
        }
        
        
        private void UpdateParanormalTriggerStatus(bool activated)
        {
            _paranormalTriggerActivated = activated;
        }
        
        public void ExitState(TaskStateManager stateManager)
        {
            _checkHouseManager.DeActiveTriggers();
            _taskInfoManager.HideInfoTab();
            Debug.Log("Exiting CheckHouse State");
        }
    }
}