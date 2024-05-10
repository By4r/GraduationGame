using UnityEngine;

namespace Runtime.TaskStateSystem.TaskStates
{
    public class CheckHouseState : ITaskState
    {
        public void EnterState(TaskStateManager stateManager)
        {
            Debug.Log("Entering CheckHouse State");
        }

        public void UpdateState(TaskStateManager stateManager)
        {
            Debug.Log("CheckHouse State");
        }

        public void ExitState(TaskStateManager stateManager)
        {
            Debug.Log("Exiting CheckHouse State");
        }
    }
}