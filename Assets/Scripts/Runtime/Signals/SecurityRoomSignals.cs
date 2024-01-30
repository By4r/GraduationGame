using Runtime.Extensions;

using UnityEngine.Events;

namespace Runtime.Signals
{
    public class SecurityRoomSignals: MonoSingleton<SecurityRoomSignals>
    {
        public UnityAction onNextCamera = delegate { };
        public UnityAction onPreviousCamera = delegate { };
        public UnityAction onSecurityCameraOpen = delegate { };
        public UnityAction onSecurityCameraClose = delegate { };
        public UnityAction onInfoPaperPanelOpen = delegate { };
        public UnityAction onInfoPaperPanelClose = delegate { };
    }
}