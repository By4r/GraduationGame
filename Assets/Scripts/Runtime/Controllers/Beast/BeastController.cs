using System.Collections;
using Runtime.Controllers.Stamina;
using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;
using UnityEngine.AI;

namespace Runtime.Controllers.Beast
{
    public class BeastController : MonoBehaviour
    {
        public NavMeshAgent beast;
        public Transform player;

        [SerializeField] private Transform beastSpawnPoint;
        [SerializeField] private StaminaController _staminaController;
        [SerializeField] private CapturePhotoController _capturePhotoController;

       
        private float timeSinceChaseStarted;

        [SerializeField] private float timeToWaitBeforeReturn = 5f;

        private void Update()
        {
            if (_staminaController.mentalStamina <= 0 || _capturePhotoController.photoRemainCount == 0)
            {
                ChasePlayer();
            }
            else
            {
                StartCoroutine(WaitAndReturn());
            }
        }
// 3 foto çektikten sonra canavar oyuncuyu kabinin içine girene kadar kovalıyor
        private void Start()
        {
            _staminaController = FindObjectOfType<StaminaController>();
            _capturePhotoController = FindObjectOfType<CapturePhotoController>();
        }

        private void ChasePlayer()
        {
            BeastSignals.Instance.onChangeBeastAnimationState?.Invoke(BeastAnimationStates.Run);
            beast.SetDestination(player.position);
            StopAllCoroutines();
            
        }
        
        private IEnumerator WaitAndReturn()
        {
            
            yield return new WaitForSeconds(timeToWaitBeforeReturn);
            beast.speed=0;
            transform.LookAt(player);
            BeastSignals.Instance.onChangeBeastAnimationState?.Invoke(BeastAnimationStates.Idle);
            yield return new WaitForSeconds(timeToWaitBeforeReturn);
            
            beast.SetDestination(beastSpawnPoint.position);
            beast.speed=15;
            
            
        }

        private void Jumpscare()
        {
            // Jumpscare işlemleri
        }
    }
}