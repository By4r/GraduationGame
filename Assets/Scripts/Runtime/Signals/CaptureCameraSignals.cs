using Runtime.Extensions;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class CaptureCameraSignals : MonoSingleton<CaptureCameraSignals>
    {
        public UnityAction onOpenPhotoMode = delegate { };
        public UnityAction onRemovePhoto = delegate { };
        public UnityAction onShowPhoto = delegate { };
        
    }
}