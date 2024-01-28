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

        private bool isChasingPlayer;
        private float timeSinceChaseStarted;

        [SerializeField] private float timeToWaitBeforeReturn = 5f;

        private void Update()
        {
           

            if (_staminaController.mentalStamina <= 0 || _capturePhotoController.photoRemainCount == 0)
            {
                if (!isChasingPlayer)
                {
                    ChasePlayer();
                }
            }
            else
            {
                if (isChasingPlayer)
                {
                    StopChasingPlayer();
                }
                StartCoroutine(WaitAndReturn());
            }
        }

        private void Start()
        {
            _staminaController = FindObjectOfType<StaminaController>();
            _capturePhotoController = FindObjectOfType<CapturePhotoController>();
        }

        private void ChasePlayer()
        {
            BeastSignals.Instance.onChangeBeastAnimationState?.Invoke(BeastAnimationStates.Run);
            Debug.Log("running chasing");
            beast.SetDestination(player.position);
            StopAllCoroutines();
            beast.transform.LookAt(player);
        }

       

        private void StopChasingPlayer()
        {
            isChasingPlayer = false;
            beast.SetDestination(transform.position); 
            BeastSignals.Instance.onChangeBeastAnimationState?.Invoke(BeastAnimationStates.Idle);
            Debug.Log("Idle");
        }

        private IEnumerator WaitAndReturn()
        {
            yield return new WaitForSeconds(timeToWaitBeforeReturn);

            BeastSignals.Instance.onChangeBeastAnimationState?.Invoke(BeastAnimationStates.Walk);
            Debug.Log("walking");

            yield return new WaitForSeconds(timeToWaitBeforeReturn);

            Debug.Log("going spawn point by walking");
            beast.SetDestination(beastSpawnPoint.position);
            //beast.transform.LookAt(beastSpawnPoint.position);
            while (beast.remainingDistance > beast.stoppingDistance)
            {
                yield return null;
            }

            BeastSignals.Instance.onChangeBeastAnimationState?.Invoke(BeastAnimationStates.Idle);
            Debug.Log("Idle at spawn point");
        }

        private void Jumpscare()
        {
            // Jumpscare işlemleri
        }
    }
}
