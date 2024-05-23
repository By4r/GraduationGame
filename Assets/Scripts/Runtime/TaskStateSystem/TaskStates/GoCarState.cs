using Runtime.TaskStateSystem.TaskUI;
using UnityEngine;

namespace Runtime.TaskStateSystem.TaskStates
{
    public class GoCarState : ITaskState
    {
        private TaskInfoManager _taskInfoManager;
        
        public void EnterState(TaskStateManager stateManager)
        {
            Debug.Log("Entering GoCar State");

            _taskInfoManager = stateManager.GetTaskInfoManager();
            
            _taskInfoManager.SetStateForInfo("GoCar");
        }

        public void UpdateState(TaskStateManager stateManager)
        {
            Debug.Log("GoCar State");
        }

        public void ExitState(TaskStateManager stateManager)
        {
            
            _taskInfoManager.HideInfoTab();
            Debug.Log("Exiting GoCar State");
        }
    }
}