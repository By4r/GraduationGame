using UnityEngine;

namespace Runtime.TaskStateSystem.TaskStates
{
    public class UpstairsState : ITaskState
    {
        public void EnterState(TaskStateManager stateManager)
        {
            Debug.Log("Entering Upstairs State");
        }

        public void UpdateState(TaskStateManager stateManager)
        {
            Debug.Log("Upstairs State");
        }

        public void ExitState(TaskStateManager stateManager)
        {
            Debug.Log("Exiting Upstairs State");
        }
    }
}