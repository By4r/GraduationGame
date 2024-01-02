using System;
using DG.Tweening;
using Runtime.Controllers.Pool;
using Runtime.Managers;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        #endregion

        #region Private Variables
        
        private readonly string _inLight = "InsideLight";
        private bool increasemental;
        private bool decreasemental;
        #endregion

        #endregion

        private void Update()
        {
            if (decreasemental&&!increasemental)
            {
                DecreaseMentalHealth();
            }
        }
        
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag(_inLight) && !decreasemental && increasemental)
            {
                Debug.Log("IncreaseMental");
                PlayerSignals.Instance.onIncreaseMentalHealth?.Invoke();
            }
        }
        private void DecreaseMentalHealth()
        {
            PlayerSignals.Instance.onDecreaseMentalHealth?.Invoke();
            Debug.Log("DecreaseMental");
        }
        private void OnTriggerExit(Collider other)
        {
            decreasemental = true;
            increasemental= false;
        }
        private void OnTriggerEnter(Collider other)
        {
            increasemental = true;
            decreasemental = false;
        }
    }
}
