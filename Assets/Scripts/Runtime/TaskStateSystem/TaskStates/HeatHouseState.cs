using UnityEngine;

namespace Runtime.TaskStateSystem.TaskStates
{
    public class HeatHouseState : ITaskState
    {
        public void EnterState(TaskStateManager stateManager)
        {
            Debug.Log("Entering HeatHouse State");
        }

        public void UpdateState(TaskStateManager stateManager)
        {
            Debug.Log("HeatHouse State");
        }

        public void ExitState(TaskStateManager stateManager)
        {
            Debug.Log("Exiting HeatHouse State");
        }
    }
}