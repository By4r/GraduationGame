using System.Collections;
using Runtime.Controllers.Player;
using Runtime.Controllers.Subtitle;
using Runtime.SoundSystem;
using Runtime.TaskStateSystem.TaskUI;
using UnityEngine;

namespace Runtime.TaskStateSystem.TaskStates
{
    public class PickUpPhoneState : ITaskState
    {
        private PlayerPhysicsController _playerPhysicsController;
        private AudioSource _audioSource;
        private PlaySubtitle _playSubtitle;
        private TaskStateManager _stateManager;
        private TaskInfoManager _taskInfoManager;

        public void EnterState(TaskStateManager stateManager)
        {
            Debug.Log("Entering PickUpPhone State");
            _playerPhysicsController = stateManager.GetPlayerPhysicsController();
            _audioSource = stateManager.GetAudioSource();
            _playSubtitle = stateManager.GetPlaySubtitle();
            _stateManager = stateManager;
            _taskInfoManager = stateManager.GetTaskInfoManager();
            
            //_taskInfoManager.SetStateForInfo("PickUpPhone");
            
            //AudioManager.Instance.PlayStateSounds("PhoneRingSound");
            
            _stateManager.StartCoroutine(PlayPhoneRingSoundWithDelay(5.0f));
            
        }

        public void UpdateState(TaskStateManager stateManager)
        {
            Ray raycast = _playerPhysicsController.GetRaycast();
            float range = _playerPhysicsController.range;

            if (Physics.Raycast(raycast, out RaycastHit hit, range))
            {
                if (hit.collider.CompareTag("Phone"))
                {
                    Debug.Log("Phone detected");

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        AudioManager.Instance.StopStateSound();
                        
                        Debug.Log("Talking Started");

                        if (_audioSource.clip != null)
                        {
                            _audioSource.Play();
                            _playSubtitle.PlayAudioWithSubtitle("pickup_phone");
                            _stateManager.StartCoroutine(FinishPhoneAfterAudio(_audioSource.clip.length));
                        }
                    }
                    else
                    {
                        Debug.LogWarning("No audio clip assigned to audioSourceObject.");
                    }
                }
            }
        }

        
        private IEnumerator PlayPhoneRingSoundWithDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            _taskInfoManager.SetStateForInfo("PickUpPhone");
            AudioManager.Instance.PlayStateSounds("PhoneRingSound");
        }
        
        private IEnumerator FinishPhoneAfterAudio(float delay)
        {
            yield return new WaitForSeconds(delay);
            _stateManager.SetState(new CollectGarbageState());
        }

        public void ExitState(TaskStateManager stateManager)
        {
            _taskInfoManager.HideInfoTab();
            Debug.Log("Exiting PickUpPhone State");
        }
    }
}