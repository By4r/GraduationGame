using System;
using Runtime.Controllers;
using Runtime.Controllers.Player;
using Runtime.TaskStateSystem.TaskUI;
using Runtime.TaskSystem;
using UnityEngine;
using DG.Tweening;
using Runtime.Managers;
using Runtime.SoundSystem;

namespace Runtime.TaskStateSystem.TaskStates
{
    public class CheckHouseState : ITaskState
    {
        private TaskInfoManager _taskInfoManager;
        private CheckHouseManager _checkHouseManager;
        private PlayerPhysicsController _playerPhysicsController;
        private bool _paranormalTriggerActivated = false;
        private bool _paranormalTriggerExitActivate = false;
        private WoodStickController _woodStickController;
        private SleepController _sleepController;
        private TaskStateManager _stateManager;
        private PlayerManager _playerManager;
        private CamScareManager _camScareManager;

        public void EnterState(TaskStateManager stateManager)
        {
            Debug.Log("Entering CheckHouse State");
            
            _playerPhysicsController = stateManager.GetPlayerPhysicsController();
            _playerPhysicsController.SetParanormalTriggerStatusUpdateAction(UpdateParanormalTriggerStatus);
            _playerPhysicsController.SetParanormalTriggerExitStatusUpdateAction(UpdateParanormalExitTriggerStatus);

            _checkHouseManager = stateManager.GetCheckHouseManager();
            _taskInfoManager = stateManager.GetTaskInfoManager();

            _camScareManager = stateManager.GetCamScareManager();

            _sleepController = stateManager.GetSleepController();

            _playerManager = stateManager.GetPlayerManager();

            _taskInfoManager.SetStateForInfo("CheckHouse");
            _checkHouseManager.ActiveTriggers();
            
            _stateManager = stateManager;
            
        }

        public void UpdateState(TaskStateManager stateManager)
        {
            Debug.Log("CheckHouse State");
        }

        private void UpdateParanormalTriggerStatus(bool activated)
        {
            _paranormalTriggerActivated = activated;
            
            //AudioManager.Instance.PlayStateSounds("KnockingWindowSound");
            
            if (_paranormalTriggerActivated)
            {
                _checkHouseManager.ShowParanormal();
            }
        }

        private void UpdateParanormalExitTriggerStatus(bool activated)
        {
            Debug.Log($"UpdateParanormalExitTriggerStatus called with value: {activated}");
            _paranormalTriggerExitActivate = activated;

            if (_paranormalTriggerExitActivate && _paranormalTriggerActivated)
            {
                Debug.Log("Exit condition met: transitioning to CheckCameraState");
                //_checkHouseManager.HideParanormal();
                

                
                Sequence sequence = DOTween.Sequence();

                sequence.AppendCallback(() => _camScareManager.showGhost())
                    .AppendInterval(0.75f)
                    .AppendCallback(() => _sleepController.SleepCompulsory(_stateManager))
                    .AppendInterval(1f)
                    .AppendCallback(() => _camScareManager.hideGhost())
                    .AppendInterval(0.2f)
                    .AppendCallback(() =>
                    {
                        Debug.Log("Transitioning to CheckCameraState");
                        TransformToPosition(_sleepController.Outbuilding.transform);
                        _stateManager.SetState(new CheckCameraState());
                    });

            }
        }

        public void ExitState(TaskStateManager stateManager)
        {
            _checkHouseManager.DeActiveTriggers();
            _taskInfoManager.HideInfoTab();
            Debug.Log("Exiting CheckHouse State");
        }
        
        private void TransformToPosition(Transform targetPosition)
        {
            if (targetPosition == null)
            {
                Debug.LogError("Target position is null");
                return;
            }

            Debug.Log($"Player current position: {_playerManager.transform.position}");
            Debug.Log($"Target position: {targetPosition.position}");
    
            _playerManager.transform.DOMove(targetPosition.position, 0f)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    Debug.Log("Player moved to target position");
                });
        }

    }
}