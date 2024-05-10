using UnityEngine;

namespace Runtime.TaskStateSystem.TaskStates
{
    public class WateringFlowerState : ITaskState
    {
        private int _currentWateringAmount;
        private int _maxWateringAmount;
        
        public void EnterState(TaskStateManager stateManager)
        {
            Debug.Log("Entering WateringFlower State");
        }

        public void UpdateState(TaskStateManager stateManager)
        {
            Debug.Log("WateringFlower State");
        }

        public void ExitState(TaskStateManager stateManager)
        {
            Debug.Log("Exiting WateringFlower State");
        }
    }
}