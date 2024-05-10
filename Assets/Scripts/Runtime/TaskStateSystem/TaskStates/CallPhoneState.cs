using UnityEngine;

namespace Runtime.TaskStateSystem.TaskStates
{
    public class CallPhoneState: ITaskState
    {
        public void EnterState(TaskStateManager stateManager)
        {
            Debug.Log("Entering CallPhone State");
        }

        public void UpdateState(TaskStateManager stateManager)
        {
            Debug.Log("CallPhone State");
        }

        public void ExitState(TaskStateManager stateManager)
        {
            Debug.Log("Exiting CallPhone State");
        }
    }
}