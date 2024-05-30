using System;
using Runtime.Managers;
using Runtime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        // private readonly string _inLight = "InsideLight";
        // private readonly string _inSecRoom = "InsideSecurityRoom";
        private readonly string _paranormalEnter = "ParanormalEnter";
        private readonly string _paranormalExit = "ParanormalExit";
        [SerializeField] private AudioSource playerAudioSource;
        [SerializeField] private AudioSource backgroundArudiosource;
        [SerializeField] private AudioClip mentalDecreaseSound;
        [SerializeField] private AudioClip mallBackgroundSound;
        [SerializeField] private PlayerManager playerManager;
        public bool isInsideLight;
        public bool isInsideSecRoom;
        [SerializeField] private CapturePhotoController _capturePhotoController;
        private bool hasPlayedMentalDecreaseSound = false;
        
        [SerializeField] public GameObject playerEyes;
        [SerializeField] public float range;

        public GameObject anomalyPrefab;
        
        private Action<bool> _updateParanormalTriggerStatus;
        private Action<bool> _updateParanormalTriggerExitStatus;
        

        private void Update()
        {
            Debug.DrawRay(playerEyes.transform.position, playerEyes.transform.TransformDirection(Vector3.forward) * range, Color.green);
            if (isInsideLight || isInsideSecRoom)
            {
                IncreaseMentalHealth();
            }
            else 
            {
                DecreaseMentalHealth();
            }
        }
        private void IncreaseMentalHealth()
        {
            PlayerSignals.Instance.onIncreaseMentalHealth?.Invoke();
            Debug.Log("IncreaseMental");
            hasPlayedMentalDecreaseSound = false;
            playerAudioSource.Stop();
        }

        public Ray GetRaycast()
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * range, Color.green);

            return new Ray(playerEyes.transform.position, playerEyes.transform.TransformDirection(Vector3.forward * range));
        }
        
        private void DecreaseMentalHealth()
        {
            PlayerSignals.Instance.onDecreaseMentalHealth?.Invoke();
            if (!hasPlayedMentalDecreaseSound)
            {
               
                Debug.Log("DecreaseMental");
                playerAudioSource.PlayOneShot(mentalDecreaseSound);
                hasPlayedMentalDecreaseSound = true;
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            // if (other.CompareTag(_inLight))
            // {
            //     isInsideLight = true;
            //     
            // }
            // else if (other.CompareTag(_inSecRoom))
            // {
            //     isInsideSecRoom = true;
            //     _capturePhotoController.photoRemainCount = 3;
            //
            // }

            if (other.CompareTag(_paranormalEnter))
            {
                _updateParanormalTriggerStatus?.Invoke(true);
            }

            if (other.CompareTag(_paranormalExit))
            {
                _updateParanormalTriggerExitStatus?.Invoke(true);
            }
            
        }

        [Button("UPDATE PARANORMAL TRUE")]
        private void UpdateParanormal()
        {
            _updateParanormalTriggerStatus?.Invoke(true);
            _updateParanormalTriggerExitStatus?.Invoke(true);
        }
        
        private void OnTriggerExit(Collider other)
        {
            // if (other.CompareTag(_inLight))
            // {
            //     isInsideLight = false;
            // }
            // else if (other.CompareTag(_inSecRoom))
            // {
            //     isInsideSecRoom = false;
            // }
        }
        
        

        internal void SetParanormalTriggerStatusUpdateAction(Action<bool> action)
        {
            _updateParanormalTriggerStatus = action;
        }

        internal void SetParanormalTriggerExitStatusUpdateAction(Action<bool> action)
        {
            _updateParanormalTriggerExitStatus = action;
        }
    }
}

