using System;
using System.Collections;
using Runtime.Controllers.Stamina;
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

        private void Update()
        {
            if (_staminaController.mentalStamina <=0 || _capturePhotoController.photoRemainCount ==0)
            {
                ChasePlayer();
                
            }

            else 
            {
                ReturnSpawnPoint();
            }
        }

        private void Start()
        {
            _staminaController = FindObjectOfType<StaminaController>();
            _capturePhotoController = FindObjectOfType<CapturePhotoController>();
        }

        private void ChasePlayer()
        {
            beast.SetDestination(player.position);
        }

        private void ReturnSpawnPoint()
        {
            
            //beast.SetDestination(beastSpawnPoint.position); 
            StartCoroutine(WaitAndReturn());
        }
        private IEnumerator WaitAndReturn()
        {
            //beast.velocity = Vector3.zero;
            yield return new WaitForSeconds(3);
            beast.SetDestination(beastSpawnPoint.position);
            
        }
        private void Jumpscare()
        {
           //jumpscare
        }
        
        
    }
}