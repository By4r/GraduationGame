using DG.Tweening;
using Runtime.Controllers.Player;
using Runtime.TaskStateSystem.TaskUI;
using System.Collections;
using Runtime.Controllers.Subtitle;
using Runtime.SoundSystem;
using UnityEngine;

namespace Runtime.TaskStateSystem.TaskStates
{
    public class GoCarState : ITaskState
    {
        private TaskInfoManager _taskInfoManager;
        private PlayerPhysicsController _playerPhysicsController;
        private TaskStateManager _stateManager;

        private bool _isCarChecked;
        
        public void EnterState(TaskStateManager stateManager)
        {
            Debug.Log("Entering GoCar State");
            
            _playerPhysicsController = stateManager.GetPlayerPhysicsController();
            _taskInfoManager = stateManager.GetTaskInfoManager();
            
            _taskInfoManager.SetStateForInfo("GoCar");
            _stateManager = stateManager;
        }

        public void UpdateState(TaskStateManager stateManager)
        {
            Debug.Log("GoCar State");
            
            Ray raycast = _playerPhysicsController.GetRaycast();
            float range = _playerPhysicsController.range;

            if (Physics.Raycast(raycast, out RaycastHit hit, range))
            {
                if (hit.collider.CompareTag("Car"))
                {
                    Debug.Log("CAR detected");

                    if (Input.GetKey(KeyCode.E) && !_isCarChecked)
                    {
                        PlaySubtitle.Instance.PlayAudioWithSubtitle("get_warm");
                        _isCarChecked = true;
                        _playerPhysicsController.StartCoroutine(HandleCarBrokeInfo());
                    }
                }
            }
        }

        private IEnumerator HandleCarBrokeInfo()
        {
            Debug.Log("Car broke, starting delay...");
            
            yield return new WaitForSeconds(1); 

            PlayCarBrokeSpeech();
        }

        private void PlayCarBrokeSpeech()
        {

            Debug.Log("Playing car broke speech...");
            
            // CHARACTER CAR BROKE SPEECH!
            


            DOTween.To(() => 0, x => {}, 1, 5).OnComplete(() =>
            {
                _stateManager.SetState(new HeatHouseState());
            });
        }
        
        

        public void ExitState(TaskStateManager stateManager)
        {
            _taskInfoManager.HideInfoTab();
            Debug.Log("Exiting GoCar State");
        }
    }
}
