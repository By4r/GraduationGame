using DG.Tweening;
using Runtime.Controllers;
using Runtime.Controllers.Player;
using Runtime.Managers;
using Runtime.StateManagers;
using Runtime.TaskStateSystem.TaskUI;
using UnityEngine;

namespace Runtime.TaskStateSystem.TaskStates
{
    public class AtticState : ITaskState
    {
        private TaskInfoManager _taskInfoManager;
        private CamScareManager _camScareManager;
        private PlayerPhysicsController _playerPhysicsController;
        private CheckAtticManager _checkAtticManager;
        private SleepController _sleepController;
        private TaskStateManager _stateManager;
        
        private bool _paranormalTriggerExitActivate = false;

        private bool _isLetterReceived;
        
        public void EnterState(TaskStateManager stateManager)
        {
            
            Debug.Log("Entering Attic State");
            
            _playerPhysicsController = stateManager.GetPlayerPhysicsController();
            _taskInfoManager = stateManager.GetTaskInfoManager();
            _camScareManager = stateManager.GetCamScareManager();
            _checkAtticManager = stateManager.GetCheckAtticManager();
            _sleepController = stateManager.GetSleepController();
            
            _playerPhysicsController.SetParanormalTriggerExitStatusUpdateAction(UpdateParanormalExitTriggerStatus);
            
            _taskInfoManager.SetStateForInfo("Attic");

            _stateManager = stateManager;

        }

        private void UpdateParanormalExitTriggerStatus(bool activated)
        {
            Debug.Log($"UpdateParanormalExitTriggerStatus called with value: {activated}");
            _camScareManager.hideGhost();
            _paranormalTriggerExitActivate = activated;

            if (_paranormalTriggerExitActivate)
            {
                Debug.Log("Exit condition met: transitioning to CheckCameraState");
                
                Sequence sequence = DOTween.Sequence();

                sequence.AppendCallback(() => _camScareManager.showGhost())
                    .AppendInterval(0.75f)
                    .AppendCallback(() => _sleepController.SleepCompulsory(_stateManager))
                    .AppendInterval(1f)
                    .AppendCallback(() => _camScareManager.hideGhost())
                    .AppendInterval(0.2f)
                    .AppendCallback(() =>
                    {
                        _stateManager.SetState(new GoCarState());
                    });

            }
        }

        public void UpdateState(TaskStateManager stateManager)
        {
            Debug.Log("Attic State");
            
            Ray raycast = _playerPhysicsController.GetRaycast();
            float range = _playerPhysicsController.range;

            if (_isLetterReceived)
            {
                _checkAtticManager.ActiveTrigger();
                _isLetterReceived = false;
            }
            
            if (Physics.Raycast(raycast, out RaycastHit hit, range))
            {
                if (hit.collider.CompareTag("Letter"))
                {
                    Debug.Log("Letter Detected");

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        _isLetterReceived = true;
                        Debug.Log("LETTER ! Received");
                    }
                    else
                    {
                        Debug.LogWarning("NO LETTER.");
                    }
                }
            }
            
            
        }

        public void ExitState(TaskStateManager stateManager)
        {
            _taskInfoManager.HideInfoTab();
            Debug.Log("Exiting Attic State");
        }
    }
}