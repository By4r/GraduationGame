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
        #region Serialized Variables
        public NavMeshAgent beast;
        public Transform player;
        [SerializeField] private float distanceToIdle;
        [SerializeField] private Transform beastSpawnPoint;
        [SerializeField] private StaminaController _staminaController;
        [SerializeField] private CapturePhotoController _capturePhotoController;
        [SerializeField] private PlayerPhysicsController _playerPhysicsController;
        [SerializeField] private float timeToWaitBeforeReturn;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private Transform jumpscareHolder;
        
        
        #endregion
        // readonly string inSpawnPoint = "inSpawnPoint";
        // readonly string _insideLight = "InsideLight";
        #region Private

        private int _currentHolder;
        private bool isChasingPlayer;
        private float timeSinceChaseStarted;
        
        #endregion
       

       
        private void Update()
        {
            RotateTowardsMovementDirection();
            if (_staminaController.mentalStamina <= 0 
                || _capturePhotoController.photoRemainCount == 0 
                && (!_playerPhysicsController.isInsideSecRoom || !_playerPhysicsController.isInsideLight))
            {
                if (!isChasingPlayer)
                {
                    StopAllCoroutines();
                    ChasePlayer();
                    
                }
            }
            else
            {
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
            //StopAllCoroutines();
            beast.speed = 20;
            //beast.transform.LookAt(player);
            
        }

        private void StopChasingPlayer()
        {
            
            isChasingPlayer = false;
            //beast.SetDestination(transform.position); 
            beast.speed = 0;
            beast.transform.LookAt(player);

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
            
            //beast.transform.LookAt(beastSpawnPoint);
            
            
        }

        internal void Jumpscare(GameObject jumpscarePrefab)
        {
            

            Debug.LogWarning("Current Holder: " + _currentHolder);
            
            Transform holderTransform = jumpscareHolder;

            // If there is already a child (anomaly) under this holder, do not instantiate again
            if (holderTransform.childCount > 0)
            {
                Debug.LogWarning("jumpscare already exists in the holder: " + holderTransform.GetChild(0).name);
                return;
            }

            // Instantiate a new anomaly under the specified holder
            var spawnJumpscare = Instantiate(jumpscarePrefab, holderTransform);

            Debug.LogWarning("jumpscare spawned and parented to the holder: " + spawnJumpscare.name);
        }
       
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void UnSubscribeEvents()
        {
            //BeastSignals.Instance.onBeastJumpscare -= Jumpscare;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void SubscribeEvents()
        {
            //BeastSignals.Instance.onBeastJumpscare += Jumpscare;
        }

    }
}
