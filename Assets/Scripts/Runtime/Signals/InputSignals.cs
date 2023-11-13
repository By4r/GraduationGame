using Runtime.Extensions;
using Runtime.Keys;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class InputSignals : MonoSingleton<InputSignals>
    {
        public UnityAction onEnableInput = delegate { };
        public UnityAction onDisableInput = delegate { };
    }
}