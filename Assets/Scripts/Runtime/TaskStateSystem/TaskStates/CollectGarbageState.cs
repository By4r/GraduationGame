using Runtime.Controllers.Player;
using Runtime.TaskStateSystem.TaskUI;
using UnityEngine;

namespace Runtime.TaskStateSystem.TaskStates
{
    public class CollectGarbageState : ITaskState
    {
        private int _currentGarbageAmount;
        private int _maxGarbageAmount;
        private PlayerPhysicsController _playerPhysicsController;
        private TaskInfoManager _taskInfoManager;
        

        public void EnterState(TaskStateManager stateManager)
        {
            Debug.Log("Entering Collect Garbage State");

            _maxGarbageAmount = stateManager.GetWorkData().MaxGarbageAmount;
            _playerPhysicsController = stateManager.GetPlayerPhysicsController();
            
            _taskInfoManager = stateManager.GetTaskInfoManager();
            
            _taskInfoManager.SetStateForInfoWNumber("CollectGarbage",_currentGarbageAmount,_maxGarbageAmount);
            _taskInfoManager.ShowInfoTab();
        }

        public void UpdateState(TaskStateManager stateManager)
        {
            Ray raycast = _playerPhysicsController.GetRaycast();
            float range = _playerPhysicsController.range;

            if (Physics.Raycast(raycast, out RaycastHit hit, range))
            {
                if (hit.collider.CompareTag("Collectable"))
                {
                    Debug.Log("Collectable detected");

                    if (Input.GetMouseButtonDown(0))
                    {
                        //Destroy(hit.collider.gameObject);
                        hit.collider.gameObject.SetActive(false);
                        IncreaseGarbageAmount();
                        Debug.Log("Collected the garbage");

                        if (_currentGarbageAmount >= _maxGarbageAmount)
                        {
                            stateManager.SetState(new SweepFloorState());
                            //stateManager.SetState(new SweepFloorState());
                        }
                    }
                }
            }
        }
        
        private void IncreaseGarbageAmount()
        {
            _currentGarbageAmount++;
            _taskInfoManager.SetStateForInfoWNumber("CollectGarbage", _currentGarbageAmount, _maxGarbageAmount);
        }
        

        public void ExitState(TaskStateManager stateManager)
        {
            _taskInfoManager.HideInfoTab();
            Debug.Log("Exiting Collect Garbage State");
        }
    }
}