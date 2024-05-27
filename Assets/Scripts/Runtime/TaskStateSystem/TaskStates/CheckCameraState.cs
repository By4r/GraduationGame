using DG.Tweening;
using Runtime.Controllers;
using Runtime.Managers;
using Runtime.TaskStateSystem.TaskUI;
using UnityEngine;

namespace Runtime.TaskStateSystem.TaskStates
{
    public class CheckCameraState : ITaskState
    {
        private TaskInfoManager _taskInfoManager;
        private SleepController _sleepController;
        private PlayerManager _playerManager;
        
        public void EnterState(TaskStateManager stateManager)
        {
            Debug.Log("Entering CheckCamera State");

            _taskInfoManager = stateManager.GetTaskInfoManager();
            
            _taskInfoManager.SetStateForInfo("CheckCamera");

            _sleepController = stateManager.GetSleepController();

            _playerManager = stateManager.GetPlayerManager();
            
            
        }

        public void UpdateState(TaskStateManager stateManager)
        {
            Debug.Log("CheckCamera State");
        }

        public void ExitState(TaskStateManager stateManager)
        {
            _taskInfoManager.HideInfoTab();
            Debug.Log("Exiting CheckCamera State");
        }
        
        
    }
}