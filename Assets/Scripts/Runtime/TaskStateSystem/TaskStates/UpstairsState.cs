using Runtime.TaskStateSystem.TaskUI;
using UnityEngine;

namespace Runtime.TaskStateSystem.TaskStates
{
    public class UpstairsState : ITaskState
    {
        private TaskInfoManager _taskInfoManager;
        
        public void EnterState(TaskStateManager stateManager)
        {
            Debug.Log("Entering Upstairs State");

            _taskInfoManager = stateManager.GetTaskInfoManager();
            
            _taskInfoManager.SetStateForInfo("CheckUpstairs");
        }

        public void UpdateState(TaskStateManager stateManager)
        {
            Debug.Log("Upstairs State");
        }

        public void ExitState(TaskStateManager stateManager)
        {
            _taskInfoManager.HideInfoTab();
            Debug.Log("Exiting Upstairs State");
        }
    }
}