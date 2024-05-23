using Runtime.Controllers.Player;
using Runtime.TaskStateSystem.TaskUI;
using UnityEngine;

namespace Runtime.TaskStateSystem.TaskStates
{
    public class CheckOfficeState : ITaskState
    {
        private PlayerPhysicsController _playerPhysicsController;
        private TaskInfoManager _taskInfoManager;
        
        public void EnterState(TaskStateManager stateManager)
        {
            Debug.Log("Entering CheckOffice State");
            _playerPhysicsController = stateManager.GetPlayerPhysicsController();

            _taskInfoManager = stateManager.GetTaskInfoManager();
            
            _taskInfoManager.SetStateForInfo("CheckOffice");
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
            _taskInfoManager.HideInfoTab();
            Debug.Log("Exiting CheckOffice State");
        }

        private void KeyReceived(TaskStateManager taskStateManager)
        {
            Debug.Log("Key Received !");
            
            taskStateManager.SetState(new AtticState());
        }
    }
}