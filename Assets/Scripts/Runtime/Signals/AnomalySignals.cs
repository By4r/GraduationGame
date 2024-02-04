using Runtime.Enums;
using Runtime.Extensions;
using UnityEngine.Events;


namespace Runtime.Signals
{
    public class AnomalySignals : MonoSingleton<AnomalySignals>
    {
        public UnityAction onAnomalySpawn = delegate { };

        public UnityAction<AnomalyStageTypes> onAnomalyStage = delegate { };

        public UnityAction onAnomalyReport = delegate { };

        public UnityAction onCheckAnomalyResult = delegate { };
    }
}