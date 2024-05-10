using UnityEngine;

namespace Runtime.TaskStateSystem.TaskStates
{
    public class AtticState : ITaskState
    {
        public void EnterState(TaskStateManager stateManager)
        {
            Debug.Log("Entering Attic State");
        }

        public void UpdateState(TaskStateManager stateManager)
        {
            Debug.Log("Attic State");
        }

        public void ExitState(TaskStateManager stateManager)
        {
            Debug.Log("Exiting Attic State");
        }
    }
}