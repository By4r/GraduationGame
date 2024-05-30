using Runtime.Controllers;
using Runtime.Controllers.Player;
using Runtime.TaskStateSystem.TaskUI;
using UnityEngine;
using System.Collections;
using Runtime.Controllers.Subtitle;
using Runtime.Managers;
using Runtime.TaskSystem;

namespace Runtime.TaskStateSystem.TaskStates
{
    public class HeatHouseState : ITaskState
    {
        private PlayerPhysicsController _playerPhysicsController;
        private TaskInfoManager _taskInfoManager;
        private PlayerPickUpController _playerPickUpController;
        private FirePlaceController _firePlaceController;
        private bool _isLetterRead;
        private TaskStateManager _stateManager;
        private CamScareManager _camScareManager;
        private HeatHouseManager _heatHouseManager;

        public void EnterState(TaskStateManager stateManager)
        {
            Debug.Log("Entering HeatHouse State");

            _playerPhysicsController = stateManager.GetPlayerPhysicsController();
            _playerPickUpController = stateManager.GetPlayerPickUpController();
            _taskInfoManager = stateManager.GetTaskInfoManager();
            _camScareManager = stateManager.GetCamScareManager();
            _heatHouseManager = stateManager.GetHeatHouseManager();
            _taskInfoManager.SetStateForInfo("HeatHouse");

            _firePlaceController = GameObject.FindObjectOfType<FirePlaceController>();
            _stateManager = stateManager;
        }

        public void UpdateState(TaskStateManager stateManager)
        {
            Debug.Log("HeatHouse State");

            Ray raycast = _playerPhysicsController.GetRaycast();
            float range = _playerPhysicsController.range;

            if (Physics.Raycast(raycast, out RaycastHit hit, range))
            {
                if (hit.collider.CompareTag("Wood"))
                {
                    Debug.Log("Wood detected");

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        _playerPickUpController.PlayerPickUp(hit);
                    }
                }

                if (hit.collider.CompareTag("Letter"))
                {
                    Debug.Log("Letter detected");

                    if (Input.GetKeyDown(KeyCode.E) && !_isLetterRead)
                    {
                        _isLetterRead = true;
                        _playerPhysicsController.StartCoroutine(HandleLetterRead());
                    }
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                _playerPickUpController.LeaveItem();

                if (_firePlaceController.IsInWoodArea() && _isLetterRead)
                {
                    HandleFirePlaceInteraction();
                }
            }
        }

        private void HandleFirePlaceInteraction()
        {
            Debug.Log("Wood is in the fireplace area");
            _firePlaceController.LightFire();
            _playerPhysicsController.StartCoroutine(AfterReadCamScare());
        }

        private IEnumerator HandleLetterRead()
        {
            Debug.Log("Letter read, starting delay...");
            _heatHouseManager.secondLetter.SetActive(true);
            yield return new WaitForSeconds(3);
            PlaySubtitle.Instance.PlayAudioWithSubtitle("devil_himself");
        }

       
        
        
        private IEnumerator AfterReadCamScare()
        {
            Debug.Log("Letter read, starting delay...");
            yield return new WaitForSeconds(4);
            
            _heatHouseManager.ShowParanormal();

            _playerPhysicsController.StartCoroutine(AfterShowGhost());
        }
        
        private IEnumerator AfterShowGhost()
        {
            Debug.Log("Letter read, starting delay...");
            yield return new WaitForSeconds(2);
            
            _stateManager.SetState(new CallPhoneState());
        }
        

        public void ExitState(TaskStateManager stateManager)
        {
            _taskInfoManager.HideInfoTab();
            PlaySubtitle.Instance.PlayAudioWithSubtitle("call_phone_help");
            Debug.Log("Exiting HeatHouse State");
        }
    }
}