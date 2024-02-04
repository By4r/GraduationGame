using System;
using DG.Tweening;
using Runtime.Controllers.Beast;
using Runtime.Controllers.Pool;
using Runtime.Enums;
using Runtime.Managers;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.Beast
{
    public class BeastPhysicsController : MonoBehaviour
    {
        private readonly string _player = "Player";
        [SerializeField] private BeastController beastController;
        [SerializeField] private GameObject jumpscarePrefab;
        [SerializeField] private AudioSource beastAudioSource;
        [SerializeField] private AudioClip jumpscareSound;
        [SerializeField] private AudioClip laughSound;
        
        
        private void OnTriggerEnter(Collider other)
        {
            
            if (other.CompareTag(_player))
            { 
                beastController.Jumpscare(jumpscarePrefab);
                Debug.Log("JUMPSCARE");
                beastAudioSource.PlayOneShot(jumpscareSound);
                beastAudioSource.PlayOneShot(laughSound);
                CoreGameSignals.Instance.onLevelFailed.Invoke();
            }
        }
        
    }
}