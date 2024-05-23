using Runtime.TaskStateSystem.TaskUI;
using UnityEngine;

namespace Runtime.TaskStateSystem.TaskStates
{
    public class AtticState : ITaskState
    {
        private TaskInfoManager _taskInfoManager;
        
        public void EnterState(TaskStateManager stateManager)
        {
            Debug.Log("Entering Attic State");
            
            _taskInfoManager = stateManager.GetTaskInfoManager();
            
            _taskInfoManager.SetStateForInfo("Attic");
        }

        public void UpdateState(TaskStateManager stateManager)
        {
            Debug.Log("Attic State");
        }

        public void ExitState(TaskStateManager stateManager)
        {
            _taskInfoManager.HideInfoTab();
            Debug.Log("Exiting Attic State");
        }
    }
}