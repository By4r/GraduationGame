using System;
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
        [SerializeField] private float distanceToIdle;
        [SerializeField] private Transform beastSpawnPoint;
        [SerializeField] private StaminaController _staminaController;
        [SerializeField] private CapturePhotoController _capturePhotoController;
        [SerializeField] private PlayerPhysicsController _playerPhysicsController;
        readonly string inSpawnPoint = "inSpawnPoint";
        readonly string _insideLight = "InsideLight";
        private bool isChasingPlayer;
        private float timeSinceChaseStarted;

        [SerializeField] private float timeToWaitBeforeReturn;
        [SerializeField] private float rotationSpeed;
        private void Update()
        {
            RotateTowardsMovementDirection();
            if (_staminaController.mentalStamina <= 0 
                || _capturePhotoController.photoRemainCount == 0 
                && !_playerPhysicsController.isInsideSecRoom )
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
        private void RotateTowardsMovementDirection()
        {
            Vector3 moveDirection = beast.velocity.normalized;
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            
            beast.transform.rotation = Quaternion.Slerp(beast.transform.rotation, toRotation, Time.deltaTime * rotationSpeed);
        }

        private void Start()
        {
            _staminaController = FindObjectOfType<StaminaController>();
            _capturePhotoController = FindObjectOfType<CapturePhotoController>();
            _playerPhysicsController = FindObjectOfType<PlayerPhysicsController>();
            beast.SetDestination(beastSpawnPoint.position);
        }

        private void ChasePlayer()
        {
            BeastSignals.Instance.onChangeBeastAnimationState?.Invoke(BeastAnimationStates.Run);
            Debug.Log("running chasing");
            beast.SetDestination(player.position);
            StopAllCoroutines();
            beast.speed = 20;
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
            Debug.Log("going spawn point by walking");
            beast.speed = 10;
            beast.SetDestination(beastSpawnPoint.position);
            BeastSignals.Instance.onChangeBeastAnimationState?.Invoke(BeastAnimationStates.Walk);
            
        }

        private void Jumpscare()
        {
           
        }
    }
}
