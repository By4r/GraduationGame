using Sirenix.OdinInspector;
using UnityEngine;

namespace Runtime.Controllers
{
    public class BroomController : MonoBehaviour
    {
        [SerializeField] Animator _animator;
        [SerializeField] private ParticleSystem _particle;

        private bool isSweeping;

        [Button("Sweep Floor!")]
        internal void SweepFloor()
        {
            if (!isSweeping)
            {
                isSweeping = true;
                _animator.SetBool("isSweeping", true);
                _particle.Play();
            }
        }

        [Button("Stop Sweep Floor!")]
        internal void StopSweepFloor()
        {
<<<<<<< HEAD
            if (isSweeping)
            {
                isSweeping = false;
                _animator.SetBool("isSweeping", false);
                _particle.Stop();
            }
=======
            _animator.ResetTrigger("sweepFloor"); 
            _particle.Stop();
>>>>>>> 0b7291e7a8ed67c70d636155ab20cda3a34be5d8
        }
    }
}