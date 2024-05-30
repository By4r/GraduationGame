using Runtime.Controllers;
using Runtime.Controllers.Player;
using Runtime.TaskStateSystem.TaskUI;
using UnityEngine;
using System.Collections;
using Runtime.Managers;

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

        public void EnterState(TaskStateManager stateManager)
        {
            Debug.Log("Entering HeatHouse State");

            _playerPhysicsController = stateManager.GetPlayerPhysicsController();
            _playerPickUpController = stateManager.GetPlayerPickUpController();
            _taskInfoManager = stateManager.GetTaskInfoManager();
            _camScareManager = stateManager.GetCamScareManager();
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
            yield return new WaitForSeconds(1);
            HeatHouseSpeech();
        }

        private void HeatHouseSpeech()
        {
            Debug.Log("Playing HeatHouse speech...");
            // Konuşma metodu. Burada bir konuşma sistemi veya animasyon ekleyebilirsiniz.
        }
        
        
        private IEnumerator AfterReadCamScare()
        {
            Debug.Log("Letter read, starting delay...");
            yield return new WaitForSeconds(4);
            
            _camScareManager.showGhostTemporary();

            _playerPhysicsController.StartCoroutine(AfterCamScare());
        }
        
        private IEnumerator AfterCamScare()
        {
            Debug.Log("Letter read, starting delay...");
            yield return new WaitForSeconds(2);
            
            _stateManager.SetState(new CallPhoneState());
        }
        

        public void ExitState(TaskStateManager stateManager)
        {
            _taskInfoManager.HideInfoTab();
            Debug.Log("Exiting HeatHouse State");
        }
    }
}