using Runtime.Controllers;
using Runtime.Controllers.Player;
using Runtime.TaskStateSystem.TaskUI;
using UnityEngine;

namespace Runtime.TaskStateSystem.TaskStates
{
    public class WateringFlowerState : ITaskState
    {
        private int _currentWateringAmount;
        private int _maxWateringAmount;
        private PlayerPhysicsController _playerPhysicsController;
        private WaterCanController _waterCanController;

        private bool isWateringActive;
        private TaskInfoManager _taskInfoManager;

        public void EnterState(TaskStateManager stateManager)
        {
            Debug.Log("Entering WateringFlower State");
            _maxWateringAmount = stateManager.GetWorkData().MaxWateringAmount;
            _playerPhysicsController = stateManager.GetPlayerPhysicsController();
            _waterCanController = Object.FindObjectOfType<WaterCanController>();
            
            _taskInfoManager = stateManager.GetTaskInfoManager();
            
            _taskInfoManager.SetStateForInfoWNumber("WateringFlowers",_currentWateringAmount,_maxWateringAmount);
            
            _taskInfoManager.ShowInfoTab();

            _waterCanController.OnWateringAmountChanged += HandleWateringAmountChanged;
        }

        public void UpdateState(TaskStateManager stateManager)
        {
            Debug.Log("WateringFlower State");

            Ray raycast = _playerPhysicsController.GetRaycast();
            float range = _playerPhysicsController.range;

            if (isWateringActive)
            {
                _waterCanController.WateringAmount();
                
            }
            
            if (Input.GetMouseButtonDown(0))
            {
                _waterCanController.WaterFlowers();
                
                if (Physics.Raycast(raycast, out RaycastHit hit, range))
                {
                    if (hit.collider.CompareTag("WateringArea"))
                    {
                        isWateringActive = true;
                        Debug.Log("Watering Area!");
                        
                        //_waterCanController.WateringAmount();
                    }
                }
            }else if (Input.GetMouseButtonUp(0))
            {
                isWateringActive = false;
                _waterCanController.StopWaterFlowers();
            }
            
            _currentWateringAmount = _waterCanController.GetCurrentWateringAmount();
            
            if (_currentWateringAmount >= _maxWateringAmount)
            {
                _waterCanController.StopWaterFlowers();
                stateManager.SetState(new GoSleepState());
            }
            
        }

        private void HandleWateringAmountChanged(int newAmount)
        {
            _currentWateringAmount = newAmount;
            _taskInfoManager.SetStateForInfoWNumber("WateringFlowers", _currentWateringAmount, _maxWateringAmount);
        }

        public void ExitState(TaskStateManager stateManager)
        {
            _taskInfoManager.HideInfoTab();
            Debug.Log("Exiting WateringFlower State");
            _waterCanController.OnWateringAmountChanged -= HandleWateringAmountChanged;
        }
    }
}