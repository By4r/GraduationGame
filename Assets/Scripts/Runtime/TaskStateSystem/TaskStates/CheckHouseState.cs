using Runtime.TaskStateSystem.TaskUI;
using UnityEngine;

namespace Runtime.TaskStateSystem.TaskStates
{
    public class CheckHouseState : ITaskState
    {
        private TaskInfoManager _taskInfoManager;
        
        public void EnterState(TaskStateManager stateManager)
        {
            Debug.Log("Entering CheckHouse State");
            
            _taskInfoManager = stateManager.GetTaskInfoManager();
            
            _taskInfoManager.SetStateForInfo("CheckHouse");
        }

        public void UpdateState(TaskStateManager stateManager)
        {
            Debug.Log("CheckHouse State");
        }

        public void ExitState(TaskStateManager stateManager)
        {
            _taskInfoManager.HideInfoTab();
            Debug.Log("Exiting CheckHouse State");
        }
    }
}