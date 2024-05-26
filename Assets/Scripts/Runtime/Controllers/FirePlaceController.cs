using System;
using UnityEngine;

namespace Runtime.Controllers
{
    public class FirePlaceController:MonoBehaviour
    {
        private bool inWoodArea = false;

        [SerializeField] private ParticleSystem fireParticle;
        [SerializeField] private AudioSource fireAudioSource;

        private void Start()
        {
            fireParticle.Stop();
            fireAudioSource.Stop();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Wood"))
            {
                inWoodArea = true;
            }
        }

        public bool IsInWoodArea()
        {
            return inWoodArea;
        }

        internal void LightFire()
        {
            fireParticle.Play();
            fireAudioSource.Play();
        }
    }
}