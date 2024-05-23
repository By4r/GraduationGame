using Runtime.Controllers;
using Runtime.Controllers.Player;
using Runtime.Controllers.UI;
using Runtime.TaskStateSystem.TaskUI;
using UnityEngine;

namespace Runtime.TaskStateSystem.TaskStates
{
    public class SweepFloorState : ITaskState
    {
        private int _currentSweepAmount;
        private int _maxSweepAmount;
        private PlayerPhysicsController _playerPhysicsController;
        private BroomController _broomController;
        //private ItemProgressBar _itemProgressBar;
        private float _sweepHoldTime;
        private const float _requiredHoldTime = 3f;
        private TaskInfoManager _taskInfoManager;

        public void EnterState(TaskStateManager stateManager)
        {
            Debug.Log("Entering SweepFloor State");
            _maxSweepAmount = stateManager.GetWorkData().MaxSweepAmount;
            _playerPhysicsController = stateManager.GetPlayerPhysicsController();
            _broomController = Object.FindObjectOfType<BroomController>();
            //_itemProgressBar = Object.FindObjectOfType<ItemProgressBar>();
            _sweepHoldTime = 0f;
            
            _taskInfoManager = stateManager.GetTaskInfoManager();
            
            _taskInfoManager.SetStateForInfo("SweepFloor");
        }

        public void UpdateState(TaskStateManager stateManager)
        {
            Debug.Log("SweepFloor State");

            Ray raycast = _playerPhysicsController.GetRaycast();
            float range = _playerPhysicsController.range;

            if (Physics.Raycast(raycast, out RaycastHit hit, range))
            {
                if (Input.GetMouseButton(0)) 
                {
                    _broomController.SweepFloor();
                    Debug.Log(_sweepHoldTime+"---"+_requiredHoldTime);
                    
                        if (hit.collider.CompareTag("SweepArea"))
                        {
                            _sweepHoldTime += Time.deltaTime;
                            
                            //_itemProgressBar.ProgressBar();
                            Debug.Log("Sweeping working");
                            if (_sweepHoldTime >= _requiredHoldTime)
                            {
                                _currentSweepAmount++;
                                hit.collider.gameObject.SetActive(false);
                                Debug.Log("Swept away waste");
                                _sweepHoldTime = 0f; 
                                
                            }

                            if (_currentSweepAmount >= _maxSweepAmount)
                            {
                                stateManager.SetState(new WateringFlowerState());
                                _broomController.StopSweepFloor();
                               
                            }
                        }
                }
                else
                {
                    _broomController.StopSweepFloor();
                    Debug.Log("Sweeping stop");
                    _sweepHoldTime = 0f; 
                }
            }
        }

        public void ExitState(TaskStateManager stateManager)
        {
            _taskInfoManager.HideInfoTab();
            Debug.Log("Exiting SweepFloor State");
        }
    }
}
