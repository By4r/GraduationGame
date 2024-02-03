using System;
using DG.Tweening;
using Runtime.Controllers.Beast;
using Runtime.Controllers.Pool;
using Runtime.Enums;
using Runtime.Managers;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        private readonly string _inLight = "InsideLight";
        private readonly string _inSecRoom = "InsideSecurityRoom";
        
        
        public bool isInsideLight;
        public bool isInsideSecRoom;
        [SerializeField] private CapturePhotoController _capturePhotoController;
        
        
       
        private void Start()
        {
            _capturePhotoController = FindObjectOfType<CapturePhotoController>();
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
        }

        private void DecreaseMentalHealth()
        {
            PlayerSignals.Instance.onDecreaseMentalHealth?.Invoke();
            Debug.Log("DecreaseMental");
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

