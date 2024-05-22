using Sirenix.OdinInspector;
using UnityEngine;

namespace Runtime.Controllers
{
    public class BroomController : MonoBehaviour
    {
        [SerializeField] Animator _animator;
        [SerializeField] private ParticleSystem _particle;


        
        [Button("Sweep Floor!")]
        internal void SweepFloor()
        {
            _animator.SetTrigger("sweepFloor");
            _particle.Play();
        }

        [Button("Stop Sweep Floor!")]
        internal void StopSweepFloor()
        {
            _animator.ResetTrigger("sweepFloor");
            _particle.Stop();
        }
        
    }
}