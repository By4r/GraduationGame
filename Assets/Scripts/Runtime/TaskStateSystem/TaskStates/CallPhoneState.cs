using Runtime.TaskStateSystem.TaskUI;
using UnityEngine;

namespace Runtime.TaskStateSystem.TaskStates
{
    public class CallPhoneState: ITaskState
    {
        private TaskInfoManager _taskInfoManager;
        public void EnterState(TaskStateManager stateManager)
        {
            Debug.Log("Entering CallPhone State");

            _taskInfoManager = stateManager.GetTaskInfoManager();
            
            _taskInfoManager.SetStateForInfo("CallPhone");
        }

        public void UpdateState(TaskStateManager stateManager)
        {
            Debug.Log("CallPhone State");
        }

        public void ExitState(TaskStateManager stateManager)
        {
            _taskInfoManager.HideInfoTab();
            
            Debug.Log("Exiting CallPhone State");
        }
    }
}