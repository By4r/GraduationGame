using UnityEngine;

namespace Runtime.Controllers
{
    public class WaterCanController:MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private ParticleSystem _particle;

        #region  Private Variables
        
        private float wateringTime;
        private float maxWateringTime = 3f;
        private bool isWatering;
        private bool hasWatered;

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

            if (isWatering)
            {
                wateringTime += Time.deltaTime;
                if (wateringTime >= maxWateringTime && !hasWatered)
                {
                    Debug.Log("Watering Done!");
                    hasWatered = true;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                _animator.SetTrigger("downCan");
                isWatering = false;
                if (_particle != null)
                {
                    _particle.Stop();
                }
            }
        }
    }
}