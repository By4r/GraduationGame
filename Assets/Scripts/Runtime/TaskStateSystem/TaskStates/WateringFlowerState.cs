using Runtime.Controllers;
using Runtime.Controllers.Player;
using UnityEngine;

namespace Runtime.TaskStateSystem.TaskStates
{
    public class WateringFlowerState : ITaskState
    {
        private int _currentWateringAmount;
        private int _maxWateringAmount;
        private PlayerPhysicsController _playerPhysicsController;
        private WaterCanController _waterCanController;
        
        public void EnterState(TaskStateManager stateManager)
        {
            Debug.Log("Entering WateringFlower State");
            _maxWateringAmount = stateManager.GetWorkData().MaxWateringAmount;
            _playerPhysicsController = stateManager.GetPlayerPhysicsController();
            _waterCanController = Object.FindObjectOfType<WaterCanController>();
        }

        public void UpdateState(TaskStateManager stateManager)
        {
            Debug.Log("WateringFlower State");
            
            Ray raycast = _playerPhysicsController.GetRaycast();
            float range = _playerPhysicsController.range;
            
            if (Physics.Raycast(raycast, out RaycastHit hit, range))
            {
                if (hit.collider.CompareTag("WateringArea"))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        
                        Debug.Log("Watering Area!");
                        
                        _waterCanController.WaterFlowers();
                        
                        //hit.collider.gameObject.SetActive(false);
                        _currentWateringAmount++;
                        
                        if (_currentWateringAmount>= _maxWateringAmount)
                        {
                            stateManager.SetState(new GoSleepState());
                        }
                    }
                }
            }
        }

        public void ExitState(TaskStateManager stateManager)
        {
            Debug.Log("Exiting WateringFlower State");
        }
    }
}