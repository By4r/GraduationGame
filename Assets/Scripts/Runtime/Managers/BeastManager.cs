using Runtime.Controllers.Beast;
using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class BeastManager : MonoBehaviour
    {
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            BeastSignals.Instance.onBeastChase += OnBeastChase;
            BeastSignals.Instance.onBeastJumpscare += OnBeastJumpscare;
            BeastSignals.Instance.onBeastReturn += OnBeastReturn;
        }
        private void UnSubscribeEvents()
        {
            BeastSignals.Instance.onBeastChase -= OnBeastChase;
            BeastSignals.Instance.onBeastJumpscare -= OnBeastJumpscare;
            BeastSignals.Instance.onBeastReturn -= OnBeastReturn;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void OnBeastChase()
        {
            BeastSignals.Instance.onBeastChase?.Invoke();
        }

        private void OnBeastJumpscare(GameObject jumpscarePrefab)
        {
            BeastSignals.Instance.onBeastJumpscare?.Invoke(jumpscarePrefab);
        }

        private void OnBeastReturn()
        {
            BeastSignals.Instance.onBeastReturn?.Invoke();
            
        }

    }
}