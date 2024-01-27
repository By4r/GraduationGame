using Runtime.Controllers.Beast;
using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class BeastManager : MonoBehaviour
    {
        
        #region Serialized Variables
        [SerializeField] private BeastAnimationController beastAnimationController;
       
        #endregion
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            //CoreGameSignals.Instance.onReset += OnReset;
            BeastSignals.Instance.onBeastChase += OnBeastChase;
            BeastSignals.Instance.onBeastJumpscare += OnBeastJumpscare;
            BeastSignals.Instance.onBeastReturn += OnBeastReturn;
        }
        private void UnSubscribeEvents()
        {
           // CoreGameSignals.Instance.onReset -= OnReset;
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

        private void OnBeastJumpscare()
        {
            BeastSignals.Instance.onBeastJumpscare?.Invoke();
        }

        private void OnBeastReturn()
        {
            BeastSignals.Instance.onBeastReturn?.Invoke();
            BeastSignals.Instance.onChangeBeastAnimationState?.Invoke(BeastAnimationStates.Walk);
        }
        private void OnReset()
        {
            beastAnimationController.OnReset();
        }

    }
}