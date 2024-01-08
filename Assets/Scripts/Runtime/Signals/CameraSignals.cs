using Runtime.Extensions;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class CameraSignals : MonoSingleton<CameraSignals>
    {
        public UnityAction onSetCameraTarget = delegate { };
        public UnityAction onCameraLocked = delegate { };
        public UnityAction onCameraConfine = delegate { };
    }
}