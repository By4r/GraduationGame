using Runtime.Enums;
using Runtime.Extensions;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class BeastSignals : MonoSingleton<BeastSignals>
    {
        public UnityAction onBeastChase = delegate { };
        public UnityAction onBeastReturn = delegate { };
        public UnityAction onBeastJumpscare = delegate { };
        public UnityAction<BeastAnimationStates> onChangeBeastAnimationState = delegate { };
        
    }
}