using Runtime.Extensions;
using UnityEngine.Events;


namespace Runtime.Signals
{
    public class TimeSignals : MonoSingleton<TimeSignals>
    {
        public UnityAction onTimeStarted = delegate {  };
        public UnityAction onTimeEnded = delegate {  };
    }
}