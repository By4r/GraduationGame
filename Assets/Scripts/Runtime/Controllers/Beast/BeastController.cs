using System.Collections;
using Runtime.Controllers.Player;
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
        public Transform beastSpawnPoint;
        public float timeToWaitBeforeReturn;
        public float rotationSpeed;
        public Transform jumpscareHolder;
        public AudioSource beastAudioSource;
        public AudioClip wakingupSound;
        public AudioClip beastFollowSound;

        private StaminaController _staminaController;
        private CapturePhotoController _capturePhotoController;
        private PlayerPhysicsController _playerPhysicsController;
        private bool isChasingPlayer;
        private bool hasPlayedBeastFollowSound;

        /*private void Update()
        {
            RotateTowardsMovementDirection();
            if (ShouldChasePlayer())
            {
                StopAllCoroutines();
                ChasePlayer();
            }
            else
            {
                StartCoroutine(WaitAndReturn());
            }
        }

        private bool ShouldChasePlayer()
        {
            return _staminaController.mentalStamina <= 0
                || _capturePhotoController.photoRemainCount == 0
                && (!_playerPhysicsController.isInsideSecRoom || !_playerPhysicsController.isInsideLight);
        }

        private void RotateTowardsMovementDirection()
        {
            Vector3 moveDirection = beast.velocity.normalized;
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            beast.transform.rotation = Quaternion.Slerp(beast.transform.rotation, toRotation, Time.deltaTime * rotationSpeed);
        }

        private void Start()
        {
            InitializeControllers();
            beast.SetDestination(beastSpawnPoint.position);
        }

        private void InitializeControllers()
        {
            _staminaController = FindObjectOfType<StaminaController>();
            _capturePhotoController = FindObjectOfType<CapturePhotoController>();
            _playerPhysicsController = FindObjectOfType<PlayerPhysicsController>();
        }

        private void ChasePlayer()
        {
            BeastSignals.Instance.onChangeBeastAnimationState?.Invoke(BeastAnimationStates.Run);
            Debug.Log("running chasing");
            beast.SetDestination(player.position);
            beast.speed = 20;
            if (!hasPlayedBeastFollowSound)
            {
                beastAudioSource.PlayOneShot(wakingupSound);
                beastAudioSource.PlayOneShot(beastFollowSound);
                hasPlayedBeastFollowSound = true;
            }
        }

        private void StopChasingPlayer()
        {
            isChasingPlayer = false;
            beast.speed = 0;
            beast.transform.LookAt(player);
            hasPlayedBeastFollowSound = false;
            BeastSignals.Instance.onChangeBeastAnimationState?.Invoke(BeastAnimationStates.Idle);
            Debug.Log("Idle");
        }

        private IEnumerator WaitAndReturn()
        {
            StopChasingPlayer();
            yield return new WaitForSeconds(timeToWaitBeforeReturn);
            BeastSignals.Instance.onChangeBeastAnimationState?.Invoke(BeastAnimationStates.Walk);
            Debug.Log("going spawn point by walking");
            beast.speed = 10;
            beast.SetDestination(beastSpawnPoint.position);
        }

        internal void Jumpscare(GameObject jumpscarePrefab)
        {
            Instantiate(jumpscarePrefab, jumpscareHolder);
        }*/
        
    }
}
