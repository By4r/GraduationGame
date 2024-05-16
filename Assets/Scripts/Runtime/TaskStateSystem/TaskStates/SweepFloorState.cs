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
        private PlayerPickUpController _playerPickUpController;
        
        public void EnterState(TaskStateManager stateManager)
        {
            Debug.Log("Entering SweepFloor State");
            _maxSweepAmount = stateManager.GetWorkData().MaxSweepAmount;
            _playerPhysicsController = stateManager.GetPlayerPhysicsController();
            _playerPickUpController = stateManager.GetPlayerPickUpController();
        }

        public void UpdateState(TaskStateManager stateManager)
        {
            Debug.Log("SweepFloor State");
            
            Ray raycast = _playerPhysicsController.GetRaycast();
            float range = _playerPhysicsController.range;
            
            if (Physics.Raycast(raycast, out RaycastHit hit, range))
            {
                if (hit.collider.CompareTag("SweepArea"))
                {
                    //_playerPickUpController.SweepFloor();
                    
                    if (Input.GetMouseButtonDown(0))
                    {
                        
                        Debug.Log("SWEEP AREA!");
                        _playerPickUpController.SweepFloor();
                        
                        hit.collider.gameObject.SetActive(false);
                        _currentSweepAmount++;
                        Debug.Log("Swept away waste");

                        if (_currentSweepAmount >= _maxSweepAmount)
                        {
                            stateManager.SetState(new WateringFlowerState());
                        }
                    }
                }
            }
            
            
        }

        public void ExitState(TaskStateManager stateManager)
        {
            Debug.Log("Exiting SweepFloor State");
        }
    }
}