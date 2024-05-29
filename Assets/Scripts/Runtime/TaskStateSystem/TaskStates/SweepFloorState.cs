using Runtime.Controllers;
using Runtime.Controllers.Player;
using Runtime.Controllers.UI;
using Runtime.SoundSystem;
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
        private ItemProgressBar _itemProgressBar;
        private float _sweepHoldTime;
        private const float REQUİRED_HOLD_TİME = 3f;
        private TaskInfoManager _taskInfoManager;

        public void EnterState(TaskStateManager stateManager)
        {
            Debug.Log("Entering SweepFloor State");
            _maxSweepAmount = stateManager.GetWorkData().MaxSweepAmount;
            _playerPhysicsController = stateManager.GetPlayerPhysicsController();
            _broomController = Object.FindObjectOfType<BroomController>();
            _itemProgressBar = Object.FindObjectOfType<ItemProgressBar>();
            _sweepHoldTime = 0f;
            
            _taskInfoManager = stateManager.GetTaskInfoManager();
            
            _taskInfoManager.SetStateForInfoWNumber("SweepFloor",_currentSweepAmount,_maxSweepAmount);
            _taskInfoManager.ShowInfoTab();
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
                    if (Input.GetMouseButton(0))
                    {
                        _broomController.SweepFloor();
                        AudioManager.Instance.PlayStateSounds("SweepSound");
                        _sweepHoldTime += Time.deltaTime;
                        _itemProgressBar.UpdateProgress(_sweepHoldTime / REQUİRED_HOLD_TİME);
                        
                        Debug.Log("Hold Time" + _sweepHoldTime);

                        if (_sweepHoldTime >= REQUİRED_HOLD_TİME)
                        {
                            IncreaseSweepAmount();
                            hit.collider.gameObject.SetActive(false);
                            Debug.Log("Swept away waste");
                            _sweepHoldTime = 0f;

                            if (_currentSweepAmount >= _maxSweepAmount)
                            {
                                stateManager.SetState(new WateringFlowerState());
                                _broomController.StopSweepFloor();
                            }
                        }
                    }
                    else
                    {
                        ResetSweep();
                    }
                }
                else
                {
                    ResetSweep();
                }
            }
        }

        private void IncreaseSweepAmount()
        {
            _currentSweepAmount++;
            _taskInfoManager.SetStateForInfoWNumber("SweepFloor", _currentSweepAmount,_maxSweepAmount);
        }

        private void ResetSweep()
        {
            AudioManager.Instance.StopStateSound();
            _broomController.StopSweepFloor();
            _sweepHoldTime = 0f;
            _itemProgressBar.ResetProgress();
        }
        
        public void ExitState(TaskStateManager stateManager)
        {
            _taskInfoManager.HideInfoTab();
            Debug.Log("Exiting SweepFloor State");
            _itemProgressBar.ResetProgress();
        }
    }
}
