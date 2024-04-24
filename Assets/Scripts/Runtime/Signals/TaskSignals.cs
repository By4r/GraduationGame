using Enums;
using Runtime.Enums;
using Runtime.Extensions;
using UnityEngine.Events;


namespace Runtime.Signals
{
    public class TaskSignals : MonoSingleton<TaskSignals>
    {
        public UnityAction onCollectGarbage = delegate { };
        public UnityAction<bool> onSleepDone = delegate { };
    }
}