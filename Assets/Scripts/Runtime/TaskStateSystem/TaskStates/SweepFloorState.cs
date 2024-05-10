using UnityEngine;

namespace Runtime.TaskStateSystem.TaskStates
{
    public class SweepFloorState : ITaskState
    {
        private int _currentSweepAmount;
        private int _maxSweepAmount;
        
        public void EnterState(TaskStateManager stateManager)
        {
            Debug.Log("Entering SweepFloor State");
        }

        public void UpdateState(TaskStateManager stateManager)
        {
            Debug.Log("SweepFloor State");
        }

        public void ExitState(TaskStateManager stateManager)
        {
            Debug.Log("Exiting SweepFloor State");
        }
    }
}