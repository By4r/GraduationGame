using Runtime.Controllers;
using Runtime.Controllers.Player;
using UnityEditor.Build.Content;
using UnityEngine;

namespace Runtime.TaskStateSystem.TaskStates
{
    public class SweepFloorState : ITaskState
    {
        private int _currentSweepAmount;
        private int _maxSweepAmount;
        private PlayerPhysicsController _playerPhysicsController;
        private BroomController _broomController;

        public void EnterState(TaskStateManager stateManager)
        {
            Debug.Log("Entering SweepFloor State");
            _maxSweepAmount = stateManager.GetWorkData().MaxSweepAmount;
            _playerPhysicsController = stateManager.GetPlayerPhysicsController();
            _broomController = Object.FindObjectOfType<BroomController>();
        }

        public void UpdateState(TaskStateManager stateManager)
        {
            Debug.Log("SweepFloor State");

            Ray raycast = _playerPhysicsController.GetRaycast();
            float range = _playerPhysicsController.range;

            if (Physics.Raycast(raycast, out RaycastHit hit, range))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _broomController.SweepFloor();
                    Debug.Log("Sweeping working");

                    if (hit.collider.CompareTag("SweepArea"))
                    {
                        Debug.Log("SWEEP AREA!");
                        
                        hit.collider.gameObject.SetActive(false);
                        _currentSweepAmount++;

                        Debug.Log("Swept away waste");
                    }
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    _broomController.StopSweepFloor();
                    
                    Debug.Log("Sweeping stop");
                }
            }
            
            if (_currentSweepAmount >= _maxSweepAmount)
            {
                stateManager.SetState(new WateringFlowerState());
            }
        }

        public void ExitState(TaskStateManager stateManager)
        {
            Debug.Log("Exiting SweepFloor State");
        }
    }
}