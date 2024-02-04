using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        private readonly string _inLight = "InsideLight";
        private readonly string _inSecRoom = "InsideSecurityRoom";
        [SerializeField] private AudioSource playerAudioSource;
        [SerializeField] private AudioSource backgroundArudiosource;
        [SerializeField] private AudioClip mentalDecreaseSound;
        [SerializeField] private AudioClip mallBackgroundSound;
        
        public bool isInsideLight;
        public bool isInsideSecRoom;
        [SerializeField] private CapturePhotoController _capturePhotoController;
        private bool hasPlayedMentalDecreaseSound = false;
        
       
        private void Start()
        {
            _capturePhotoController = FindObjectOfType<CapturePhotoController>();
            backgroundArudiosource.PlayOneShot(mallBackgroundSound);
        }

        private void Update()
        {
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
            if (other.CompareTag(_inLight))
            {
                isInsideLight = true;
                
            }
            else if (other.CompareTag(_inSecRoom))
            {
                isInsideSecRoom = true;
                _capturePhotoController.photoRemainCount = 3;

            }
            
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(_inLight))
            {
                isInsideLight = false;
            }
            else if (other.CompareTag(_inSecRoom))
            {
                isInsideSecRoom = false;
            }
        }

    }
}

