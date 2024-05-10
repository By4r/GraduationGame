using Runtime.Controllers;
using Runtime.Controllers.Player;
using UnityEngine;

namespace Runtime.TaskStateSystem.TaskStates
{
    public class GoSleepState : ITaskState
    {
        private SleepController _sleepController;
        
        public void EnterState(TaskStateManager stateManager)
        {
            Debug.Log("Entering GoSleep State");

            _sleepController = stateManager.GetSleepController();
        }

        public void UpdateState(TaskStateManager stateManager)
        {
            PlayerPhysicsController playerPhysicsController = stateManager.GetPlayerPhysicsController();

            Ray raycast = playerPhysicsController.GetRaycast();
            float range = playerPhysicsController.range;

            if (Physics.Raycast(raycast, out RaycastHit hit, range))
            {
                if (hit.collider.CompareTag("Bed"))
                {
                    Debug.Log("Collectable detected");

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Debug.Log("Sleeping...");
                        
                        _sleepController.Sleep(stateManager);
                        
                    }
                }
            }

        }

        public void ExitState(TaskStateManager stateManager)
        {
            Debug.Log("Exiting GoSleep State");
        }
    }
}
