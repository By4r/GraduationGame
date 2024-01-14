using Runtime.Extensions;

using UnityEngine.Events;

namespace Runtime.Signals
{
    public class SecurityCameraSignals: MonoSingleton<SecurityCameraSignals>
    {
        public UnityAction onNextCamera = delegate { };
        public UnityAction onPreviousCamera = delegate { };
        public UnityAction onSecurityCameraOpen = delegate { };
        public UnityAction onSecurityCameraClose = delegate { };
    }
}