﻿using System;
using Runtime.Controllers;
using Runtime.Controllers.Player;
using Runtime.TaskStateSystem.TaskUI;
using Runtime.TaskSystem;
using UnityEngine;
using DG.Tweening;
using Runtime.Managers;

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

            _stateManager = stateManager;
            
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
            
        }

        public void UpdateState(TaskStateManager stateManager)
        {
            Debug.Log("CheckHouse State");
        }

        private void UpdateParanormalTriggerStatus(bool activated)
        {
            _paranormalTriggerActivated = activated;
            
            if (_paranormalTriggerActivated)
            {
                _checkHouseManager.ShowParanormal();
            }
        }

        private void UpdateParanormalExitTriggerStatus(bool activated)
        {
            _paranormalTriggerExitActivate = activated;
            
            if(_paranormalTriggerExitActivate && _paranormalTriggerActivated)
            {
                Debug.Log("CIKIS KOSULU SAGLANDI");
                _checkHouseManager.HideParanormal();
                
                Sequence sequence = DOTween.Sequence();

                sequence.AppendCallback(() => _camScareManager.showGhost())
                    .AppendInterval(0.75f)
                    .AppendCallback(() => _sleepController.SleepCompulsory(_stateManager))
                    .AppendInterval(1f)
                    .AppendCallback(() => _camScareManager.hideGhost());

            }
        }
        
        public void ExitState(TaskStateManager stateManager)
        {
            
            TransformToPosition(_sleepController.Outbuilding.transform);
            _checkHouseManager.DeActiveTriggers();
            _taskInfoManager.HideInfoTab();
            Debug.Log("Exiting CheckHouse State");
        }
        
        private void TransformToPosition(Transform targetPosition)
        {
            _playerManager.transform.DOMove(targetPosition.position, 1f)
                .SetEase(Ease.InOutQuad)
                .OnComplete(() =>
                {
                    Debug.Log("Player moved to target position");
                });
        }
    }
}