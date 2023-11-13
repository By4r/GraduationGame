using Runtime.Extensions;
using UnityEngine.Events;


namespace Runtime.Signals
{
    public class AnomalySignals : MonoSingleton<AnomalySignals>
    {
        public UnityAction onAnomalySpawn = delegate {  };
    }
}