using System;
using Runtime.Signals;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Managers
{
    public class PauseManager : MonoBehaviour
    {
        #region Private Variables

        private bool _isPaused = false;

        #endregion

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            PauseSignals.Instance.onPauseGame += OnPauseGame;
            PauseSignals.Instance.onResumeGame += OnResumeGame;
        }


        private void UnSubscribeEvents()
        {
            PauseSignals.Instance.onPauseGame -= OnPauseGame;
            PauseSignals.Instance.onResumeGame -= OnResumeGame;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        void Update()
        {
            // Check if the 'Escape' key is pressed
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // Toggle between pausing and resuming the game
                if (_isPaused)
                    OnResumeGame();
                else
                    OnPauseGame();
            }
        }

        private void OnPauseGame()
        {
            CoreGameSignals.Instance.onPause?.Invoke();
            CameraSignals.Instance.onCameraConfine?.Invoke();

            // Pause the game by setting the time scale to 0
            Time.timeScale = 0f;
            // You can add additional pause-related actions here (e.g., displaying UI)
            _isPaused = true;
        }

        private void OnResumeGame()
        {
            CoreGameSignals.Instance.onResume?.Invoke();
            CameraSignals.Instance.onCameraLocked?.Invoke();


            // Resume the game by setting the time scale to 1
            Time.timeScale = 1f;
            // You can add additional actions to resume the game (e.g., removing UI)
            _isPaused = false;
        }
    }
}