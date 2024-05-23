using Runtime.TaskStateSystem.TaskUI;
using UnityEngine;

namespace Runtime.TaskStateSystem.TaskStates
{
    public class CheckCameraState : ITaskState
    {
        private TaskInfoManager _taskInfoManager;
        public void EnterState(TaskStateManager stateManager)
        {
            Debug.Log("Entering CheckCamera State");

            _taskInfoManager = stateManager.GetTaskInfoManager();
            
            _taskInfoManager.SetStateForInfo("CheckCamera");
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