using Runtime.TaskStateSystem.TaskUI;
using UnityEngine;

namespace Runtime.TaskStateSystem.TaskStates
{
    public class HeatHouseState : ITaskState
    {
        private TaskInfoManager _taskInfoManager;
        public void EnterState(TaskStateManager stateManager)
        {
            Debug.Log("Entering HeatHouse State");

            _taskInfoManager = stateManager.GetTaskInfoManager();
            
            _taskInfoManager.SetStateForInfo("HeatHouse");
        }

        public void UpdateState(TaskStateManager stateManager)
        {
            Debug.Log("HeatHouse State");
        }

        public void ExitState(TaskStateManager stateManager)
        {
            _taskInfoManager.HideInfoTab();
            
            Debug.Log("Exiting HeatHouse State");
        }
    }
}