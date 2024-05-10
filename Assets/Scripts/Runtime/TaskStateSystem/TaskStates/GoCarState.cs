using UnityEngine;

namespace Runtime.TaskStateSystem.TaskStates
{
    public class GoCarState : ITaskState
    {
        public void EnterState(TaskStateManager stateManager)
        {
            Debug.Log("Entering GoCar State");
        }

        public void UpdateState(TaskStateManager stateManager)
        {
            Debug.Log("GoCar State");
        }

        public void ExitState(TaskStateManager stateManager)
        {
            Debug.Log("Exiting GoCar State");
        }
    }
}