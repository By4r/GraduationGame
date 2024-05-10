using UnityEngine;

namespace Runtime.TaskStateSystem.TaskStates
{
    public class CheckCameraState : ITaskState
    {
        public void EnterState(TaskStateManager stateManager)
        {
            Debug.Log("Entering CheckCamera State");
        }

        public void UpdateState(TaskStateManager stateManager)
        {
            Debug.Log("CheckCamera State");
        }

        public void ExitState(TaskStateManager stateManager)
        {
            Debug.Log("Exiting CheckCamera State");
        }
    }
}