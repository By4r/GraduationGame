using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Data.ValueObjects
{
    [CreateAssetMenu(fileName = "CD_Anomaly", menuName = "GraduationGame/CD_Anomaly", order = 0)]
    public class CD_Anomaly : ScriptableObject
    {
        public AnomalyData anomalyData;
    }
}