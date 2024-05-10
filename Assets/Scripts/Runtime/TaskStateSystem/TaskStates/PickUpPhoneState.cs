using Runtime.Controllers.Player;
using UnityEngine;

namespace Runtime.TaskStateSystem.TaskStates
{
    public class PickUpPhoneState : ITaskState
    {
        private PlayerPhysicsController _playerPhysicsController;
        private AudioSource _audioSource;
        
        public void EnterState(TaskStateManager stateManager)
        {
            Debug.Log("Entering PickUpPhone State");
            _playerPhysicsController = stateManager.GetPlayerPhysicsController();
            _audioSource = stateManager.GetAudioSource();
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
                            //_audioSource.PlayOneShot();
                            //subtitleManager.StartSpeech();
                            stateManager.SetState(new CollectGarbageState());
                            FinishPhone();
                        }
                    }
                    else
                    {
                        Debug.LogWarning("No audio clip assigned to audioSourceObject.");
                    }
                    
                }
            }
        }

        private void FinishPhone()
        {
            Debug.Log("FinishPhone method called after 24 seconds.");
            
        }
        
        public void ExitState(TaskStateManager stateManager)
        {
            Debug.Log("Exiting PickUpPhone State");
        }
    }
}