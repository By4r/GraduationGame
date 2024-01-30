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
            CaptureCameraSignals.Instance.onOpenPhotoMode += OnPhotoModeOpen;
            CaptureCameraSignals.Instance.onRemovePhoto += onRemovePhoto;
            CaptureCameraSignals.Instance.onShowPhoto += onShowPhoto;
        }


        private void UnSubscribeEvents()
        {
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
        
    }
}