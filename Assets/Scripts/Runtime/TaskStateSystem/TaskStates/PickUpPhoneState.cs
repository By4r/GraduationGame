using System.Collections;
using Runtime.Controllers;
using Runtime.Controllers.Player;
using Runtime.Managers;
using UnityEngine;

namespace Runtime.TaskStateSystem.TaskStates
{
    public class PickUpPhoneState : ITaskState
    {
        private PlayerPhysicsController _playerPhysicsController;
        private AudioSource _audioSource;
        private PlaySubtitle _playSubtitle;
        private TaskStateManager _stateManager;

        public void EnterState(TaskStateManager stateManager)
        {
            Debug.Log("Entering PickUpPhone State");
            _playerPhysicsController = stateManager.GetPlayerPhysicsController();
            _audioSource = stateManager.GetAudioSource();
            _playSubtitle = stateManager.GetPlaySubtitle();
            _stateManager = stateManager;
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
                        Debug.Log("Talking Started");

                        if (_audioSource.clip != null)
                        {
                            _audioSource.Play();
                            _playSubtitle.PlaySpeech(_audioSource);
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

        private IEnumerator FinishPhoneAfterAudio(float delay)
        {
            yield return new WaitForSeconds(delay);
            _stateManager.SetState(new CollectGarbageState());
        }

        public void ExitState(TaskStateManager stateManager)
        {
            Debug.Log("Exiting PickUpPhone State");
        }
    }
}