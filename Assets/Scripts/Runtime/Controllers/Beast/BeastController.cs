using System;
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
        

        private void Update()
        {
            
            
        }

       

        private void ChasePlayer()
        {
            if (_staminaController.mentalStamina ==0 || 
                _capturePhotoController.photoRemainCount ==0)
            {
                beast.SetDestination(player.position);
            }
            
        }

        private void ReturnSpawnPoint()
        {
            beast.SetDestination(beastSpawnPoint.position);
        }

        private void Jumpscare()
        {
           //jumpscare
        }
    }
}