using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class SecurityCameraManager : MonoBehaviour
    {
        #region Serialized Variables
        
        #endregion
        
        private void OnNextCamera()
        {
            SecurityRoomSignals.Instance.onNextCamera?.Invoke();
        }
        private void OnPreviousCamera()
        {
            SecurityRoomSignals.Instance.onPreviousCamera?.Invoke();
        }

        private void OnSecurityCameraOpen()
        {
            SecurityRoomSignals.Instance.onSecurityCameraOpen?.Invoke();
        }

        private void OnSecurityCameraClose()
        {
            SecurityRoomSignals.Instance.onSecurityCameraClose?.Invoke();
        }
        private void OnInfoPaperOpen()
        {
            SecurityRoomSignals.Instance.onInfoPaperPanelOpen?.Invoke();
        }
        private void OnInfoPaperClose()
        {
            SecurityRoomSignals.Instance.onInfoPaperPanelClose?.Invoke();
        }
        private void OnPauseState(bool state)
        {
            PauseSignals.Instance.onPauseState?.Invoke(state);
        }
        
        private void SubscribeEvents()
        {
            SecurityRoomSignals.Instance.onNextCamera += OnNextCamera;
            SecurityRoomSignals.Instance.onPreviousCamera += OnPreviousCamera;
            SecurityRoomSignals.Instance.onSecurityCameraOpen += OnSecurityCameraOpen;
            SecurityRoomSignals.Instance.onSecurityCameraClose += OnSecurityCameraClose;
            SecurityRoomSignals.Instance.onInfoPaperPanelOpen += OnInfoPaperOpen;
            SecurityRoomSignals.Instance.onInfoPaperPanelClose += OnInfoPaperClose;
            PauseSignals.Instance.onPauseState += OnPauseState;

        }
        private void UnSubscribeEvents()
        {
            SecurityRoomSignals.Instance.onNextCamera -= OnNextCamera;
            SecurityRoomSignals.Instance.onPreviousCamera -= OnPreviousCamera;
            SecurityRoomSignals.Instance.onSecurityCameraOpen -= OnSecurityCameraOpen;
            SecurityRoomSignals.Instance.onSecurityCameraClose -= OnSecurityCameraClose;
            SecurityRoomSignals.Instance.onInfoPaperPanelOpen -= OnInfoPaperOpen;
            SecurityRoomSignals.Instance.onInfoPaperPanelClose -= OnInfoPaperClose;
            PauseSignals.Instance.onPauseState -= OnPauseState;
            
        }
        
        private void OnDisable()
        {
            UnSubscribeEvents();
        }
        private void OnEnable()
        {
            SubscribeEvents();
        }
        
    }
}