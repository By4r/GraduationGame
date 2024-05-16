using Runtime.Controllers.Player;
using UnityEngine;

namespace Runtime.TaskStateSystem.TaskStates
{
    public class CollectGarbageState : ITaskState
    {
        private int _currentGarbageAmount;
        private int _maxGarbageAmount;
        private PlayerPhysicsController _playerPhysicsController;

        public void EnterState(TaskStateManager stateManager)
        {
            Debug.Log("Entering Collect Garbage State");

            _maxGarbageAmount = stateManager.GetWorkData().MaxGarbageAmount;
            _playerPhysicsController = stateManager.GetPlayerPhysicsController();
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
                        _currentGarbageAmount++;
                        Debug.Log("Collected the garbage");

                        if (_currentGarbageAmount >= _maxGarbageAmount)
                        {
                            //stateManager.SetState(new GoSleepState());
                            stateManager.SetState(new SweepFloorState());
                        }
                    }
                }
            }
        }

        public void ExitState(TaskStateManager stateManager)
        {
            Debug.Log("Exiting Collect Garbage State");
        }
    }
}