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
            PauseSignals.Instance.onMainMenuGame += OnMainMenuGame;
        }


        private void UnSubscribeEvents()
        {
            PauseSignals.Instance.onPauseGame -= OnPauseGame;
            PauseSignals.Instance.onResumeGame -= OnResumeGame;
            PauseSignals.Instance.onMainMenuGame -= OnMainMenuGame;
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
            PauseSignals.Instance.onPhotoModeState?.Invoke(false);
            
            PauseSignals.Instance.onPauseState?.Invoke(true);

            // Pause the game by setting the time scale to 0
            Time.timeScale = 0f;
            // You can add additional pause-related actions here (e.g., displaying UI)
            _isPaused = true;
        }

        private void OnResumeGame()
        {
            CoreGameSignals.Instance.onResume?.Invoke();
            CameraSignals.Instance.onCameraLocked?.Invoke();
            PauseSignals.Instance.onPhotoModeState?.Invoke(true);
            
            PauseSignals.Instance.onPauseState?.Invoke(false);

            // Resume the game by setting the time scale to 1
            Time.timeScale = 1f;
            // You can add additional actions to resume the game (e.g., removing UI)
            _isPaused = false;
        }

        private void OnMainMenuGame()
        {
            CoreGameSignals.Instance.onResume?.Invoke();
            
            PauseSignals.Instance.onPauseState?.Invoke(false);
            //CameraSignals.Instance.onCameraConfine?.Invoke();


            // Resume the game by setting the time scale to 1
            Time.timeScale = 1f;
            // You can add additional actions to resume the game (e.g., removing UI)
            _isPaused = false;
        }
    }
}