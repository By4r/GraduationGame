﻿using UnityEngine;

namespace Runtime.Controllers
{
    public class WaterCanController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private ParticleSystem _particle;

        #region  Private Variables
        
        private float wateringTime;
        private float maxWateringTime = 3f;
        private bool isWatering;
        private bool hasWatered;
        private int currentWateringAmount;

        #endregion
        
        internal void WaterFlowers()
        {
            if (Input.GetMouseButtonDown(0))
            {
                isWatering = true;
                _animator.SetTrigger("upCan");
                wateringTime = 0f;
                hasWatered = false;
                if (_particle != null)
                {
                    _particle.Play();
                }
                else return;
            }
        }

        internal void WateringAmount()
        {
            if (isWatering)
            {
                Debug.Log("IS WATERING");
                wateringTime += Time.deltaTime;
                Debug.Log("Watering Time"+ wateringTime);
                if (wateringTime >= maxWateringTime && !hasWatered)
                {
                    Debug.Log("Watering Done!");
                    hasWatered = true;
                    
                    currentWateringAmount++;
                    Debug.Log("Current Watering Amount Increased!");
                }
            }
            
        }

        internal void StopWaterFlowers()
        {
            _animator.SetTrigger("downCan");
            isWatering = false;
            if (_particle != null)
            {
                _particle.Stop();
            }
        }

        internal int GetCurrentWateringAmount()
        {
            return currentWateringAmount;
        }
        
    }
}