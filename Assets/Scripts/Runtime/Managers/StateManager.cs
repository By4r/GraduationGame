using Enums;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class StateManager : MonoBehaviour
    {
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            StateSignals.Instance.onSetGameState += OnSetGameState;
        }

        private void OnSetGameState(GameStates state)
        {
            switch (state)
            {
                case GameStates.UI:
                    PauseSignals.Instance.onCanPause?.Invoke(false);
                    break;
                case GameStates.Gameplay:
                    PauseSignals.Instance.onCanPause?.Invoke(true);
                    break;
            }
        }


        private void UnSubscribeEvents()
        {
            StateSignals.Instance.onSetGameState -= OnSetGameState;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}