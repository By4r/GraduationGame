using Runtime.Signals;
using Runtime.Controllers.Security_Camera;
using UnityEngine;

namespace Runtime.Managers
{
    public class SecurityCameraManager : MonoBehaviour
    {
        #region Serialized Variables
        
        #endregion
        
        private void OnNextCamera()
        {
            SecurityCameraSignals.Instance.onNextCamera?.Invoke();
        }
        private void OnPreviousCamera()
        {
            SecurityCameraSignals.Instance.onPreviousCamera?.Invoke();
        }

        private void SubscribeEvents()
        {
            SecurityCameraSignals.Instance.onNextCamera += OnNextCamera;
            SecurityCameraSignals.Instance.onPreviousCamera += OnPreviousCamera;
        }
        private void UnSubscribeEvents()
        {
            SecurityCameraSignals.Instance.onNextCamera -= OnNextCamera;
            SecurityCameraSignals.Instance.onPreviousCamera -= OnPreviousCamera;
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