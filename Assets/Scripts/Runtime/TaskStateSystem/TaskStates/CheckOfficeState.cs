using Runtime.Controllers.Player;
using UnityEngine;

namespace Runtime.TaskStateSystem.TaskStates
{
    public class CheckOfficeState : ITaskState
    {
        private PlayerPhysicsController _playerPhysicsController;
        
        public void EnterState(TaskStateManager stateManager)
        {
            Debug.Log("Entering CheckOffice State");
            _playerPhysicsController = stateManager.GetPlayerPhysicsController();
        }

        public void UpdateState(TaskStateManager stateManager)
        {
            Debug.Log("CHECK OFFICE STATE!");

            Ray raycast = _playerPhysicsController.GetRaycast();
            float range = _playerPhysicsController.range;

            if (Physics.Raycast(raycast, out RaycastHit hit, range))
            {
                if (hit.collider.CompareTag("Key"))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        KeyReceived(stateManager);
                    }
                }
            }
        }

        public void ExitState(TaskStateManager stateManager)
        {
            Debug.Log("Exiting CheckOffice State");
        }

        private void KeyReceived(TaskStateManager taskStateManager)
        {
            Debug.Log("Key Received !");
            
            taskStateManager.SetState(new AtticState());
        }
    }
}