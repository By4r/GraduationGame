using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Runtime.Managers
{
    public class UIManager : MonoBehaviour
    {
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize += OnLevelInitialize;
            CoreGameSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
            CoreGameSignals.Instance.onLevelFailed += OnLevelFailed;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onStageAreaSuccessful += OnStageAreaSuccessful;
            CoreGameSignals.Instance.onPause += OnPauseGame;
            CoreGameSignals.Instance.onResume += OnResumeGame;
        }


        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize -= OnLevelInitialize;
            CoreGameSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
            CoreGameSignals.Instance.onLevelFailed -= OnLevelFailed;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onStageAreaSuccessful -= OnStageAreaSuccessful;
            CoreGameSignals.Instance.onPause -= OnPauseGame;
            CoreGameSignals.Instance.onResume -= OnResumeGame;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void OnLevelInitialize(byte levelValue)
        {
            UISignals.Instance.onSetLevelValue?.Invoke(levelValue);
        }

        private void OnLevelSuccessful()
        {
            CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Photo, 2);
        }

        private void OnLevelFailed()
        {
            CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Fail, 2);
        }

        private void OnSettingsPanel()
        {
            CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Fail, 2);
        }

        private void OnPauseGame()
        {
            CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Pause, 3);
            //CameraSignals.Instance.onCameraConfine?.Invoke();
        }

        private void OnResumeGame()
        {
            CoreUISignals.Instance.onClosePanel?.Invoke(3);
            //CameraSignals.Instance.onCameraLocked?.Invoke();
        }

        public void NextLevel()
        {
            CoreGameSignals.Instance.onNextLevel?.Invoke();
            CoreGameSignals.Instance.onReset?.Invoke();
        }

        public void RestartLevel()
        {
            CoreGameSignals.Instance.onRestartLevel?.Invoke();
            CoreGameSignals.Instance.onReset?.Invoke();
        }

        public void Play()
        {
            UISignals.Instance.onPlay?.Invoke();
            CoreGameSignals.Instance.onLevelStart?.Invoke();
            CoreUISignals.Instance.onClosePanel?.Invoke(1);
            CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Level, 1);
            InputSignals.Instance.onEnableInput?.Invoke();
            //CameraSignals.Instance.onCameraLocked?.Invoke();
        }

        public void Settings()
        {
            CoreUISignals.Instance.onClosePanel?.Invoke(1);
            CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Settings, 2);
        }

        public void CloseSettings()
        {
            CoreUISignals.Instance.onClosePanel?.Invoke(2);
            CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Start, 1);
        }

        public void QuitGame()
        {
            Debug.LogWarning("Quit Game!");
            Application.Quit();
        }


        [Button("RESUME BUTTON")]
        public void Resume()
        {
            //CoreUISignals.Instance.onClosePanel?.Invoke(3);
            PauseSignals.Instance.onResumeGame?.Invoke();
        }

        public void ReturnMainMenu()
        {
            PauseSignals.Instance.onMainMenuGame?.Invoke();
            CoreGameSignals.Instance.onCancelLevel?.Invoke();
        }

        public void LoadGame()
        {
            CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Loading, 1);
        }

        private void OnStageAreaSuccessful(byte stageValue)
        {
            UISignals.Instance.onSetStageColor?.Invoke(stageValue);
        }

        private void OnReset()
        {
            CoreUISignals.Instance.onCloseAllPanels?.Invoke();
            CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Start, 1);
        }
    }
}