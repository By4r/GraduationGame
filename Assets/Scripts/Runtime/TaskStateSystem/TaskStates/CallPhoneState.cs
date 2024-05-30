using Runtime.Controllers.Player;
using Runtime.Controllers.UI;
using Runtime.TaskStateSystem.TaskUI;
using UnityEngine;

namespace Runtime.TaskStateSystem.TaskStates
{
    public class CallPhoneState: ITaskState
    {
        private TaskInfoManager _taskInfoManager;
        private EndingController _endingController;
        private PlayerPhysicsController _playerPhysicsController;
        
        public void EnterState(TaskStateManager stateManager)
        {
            Debug.Log("Entering CallPhone State");

            _taskInfoManager = stateManager.GetTaskInfoManager();

            _playerPhysicsController = stateManager.GetPlayerPhysicsController();

            _endingController = stateManager.GetEndingController();
            
            _taskInfoManager.SetStateForInfo("CallPhone");
        }

        public void UpdateState(TaskStateManager stateManager)
        {
            Debug.Log("CallPhone State");
            
            Ray raycast = _playerPhysicsController.GetRaycast();
            float range = _playerPhysicsController.range;
            
            if (Physics.Raycast(raycast, out RaycastHit hit, range))
            {
                if (hit.collider.CompareTag("Phone"))
                {
                    Debug.Log("Phone detected");

                    if (!Input.GetKeyDown(KeyCode.E)) return;
                    Debug.Log("ENDING CHOICES");
                    _endingController.OpenInventoryPanel();
                }
            }
            
        }

        public void ExitState(TaskStateManager stateManager)
        {
            _taskInfoManager.HideInfoTab();
            
            Debug.Log("Exiting CallPhone State");
        }
    }
}