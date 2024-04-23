using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class CapturePhotoManager : MonoBehaviour
    {
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CaptureCameraSignals.Instance.onPhotoPanelState += OnPhotoPanelState;
            PauseSignals.Instance.onPauseState += OnPauseState;
            CaptureCameraSignals.Instance.onOpenPhotoMode += OnPhotoModeOpen;
            CaptureCameraSignals.Instance.onRemovePhoto += onRemovePhoto;
            CaptureCameraSignals.Instance.onShowPhoto += onShowPhoto;
        }


        private void UnSubscribeEvents()
        {
            CaptureCameraSignals.Instance.onPhotoPanelState -= OnPhotoPanelState;
            PauseSignals.Instance.onPauseState -= OnPauseState;
            CaptureCameraSignals.Instance.onOpenPhotoMode -= OnPhotoModeOpen;
            CaptureCameraSignals.Instance.onRemovePhoto -= onRemovePhoto;
            CaptureCameraSignals.Instance.onShowPhoto -= onShowPhoto;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
        
        private void OnPhotoModeOpen()
        {
            CaptureCameraSignals.Instance.onOpenPhotoMode?.Invoke();
            
        }
        private void onRemovePhoto()
        {
            CaptureCameraSignals.Instance.onRemovePhoto?.Invoke();
            
        }

        private void onShowPhoto()
        {
            CaptureCameraSignals.Instance.onShowPhoto?.Invoke();
        }
        
        private void OnPhotoPanelState(bool state)
        {
            CaptureCameraSignals.Instance.onPhotoPanelState?.Invoke(state);
            
        }
        private void OnPauseState(bool state)
        {
            PauseSignals.Instance.onPauseState?.Invoke(state);
            
        }
        
    }
}