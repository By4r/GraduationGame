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
       
        [SerializeField] private Transform beastSpawnPoint;
        [SerializeField] private StaminaController _staminaController;
        [SerializeField] private CapturePhotoController _capturePhotoController;
        [SerializeField] private PlayerPhysicsController _playerPhysicsController;
        [SerializeField] private float timeToWaitBeforeReturn;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private Transform jumpscareHolder;
        
        
        #endregion
       
        #region Private
        private bool isChasingPlayer;

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
                    //StopAllCoroutines();
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
            Transform holderTransform = jumpscareHolder;
            Instantiate(jumpscarePrefab, holderTransform);
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
