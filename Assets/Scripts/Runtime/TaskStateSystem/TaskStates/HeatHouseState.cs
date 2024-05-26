using Runtime.Controllers;
using Runtime.Controllers.Player;
using Runtime.TaskStateSystem.TaskUI;
using UnityEngine;

namespace Runtime.TaskStateSystem.TaskStates
{
    public class HeatHouseState : ITaskState
    {
        private PlayerPhysicsController _playerPhysicsController;
        private TaskInfoManager _taskInfoManager;
        private PlayerPickUpController _playerPickUpController;
        private FirePlaceController _firePlaceController;

        public void EnterState(TaskStateManager stateManager)
        {
            Debug.Log("Entering HeatHouse State");

            _playerPhysicsController = stateManager.GetPlayerPhysicsController();

            _playerPickUpController = stateManager.GetPlayerPickUpController();

            _taskInfoManager = stateManager.GetTaskInfoManager();

            _taskInfoManager.SetStateForInfo("HeatHouse");
            
            _firePlaceController = GameObject.FindObjectOfType<FirePlaceController>();
        }

        public void UpdateState(TaskStateManager stateManager)
        {
            Debug.Log("HeatHouse State");

            Ray raycast = _playerPhysicsController.GetRaycast();
            float range = _playerPhysicsController.range;

            if (Physics.Raycast(raycast, out RaycastHit hit, range))
            {
                if (hit.collider.CompareTag("Wood"))
                {
                    Debug.Log("Wood detected");

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        _playerPickUpController.PlayerPickUp(hit);
                    }
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                _playerPickUpController.LeaveItem();
            }

            if (_firePlaceController.IsInWoodArea())
            {
                HandleFirePlaceInteraction();
            }
            
        }

        private void HandleFirePlaceInteraction()
        {
            Debug.Log("Wood is in the fireplace area");
        }

        public void ExitState(TaskStateManager stateManager)
        {
            _taskInfoManager.HideInfoTab();

            Debug.Log("Exiting HeatHouse State");
        }
    }
}