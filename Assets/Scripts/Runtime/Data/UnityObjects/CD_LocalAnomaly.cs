using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Runtime.Data.ValueObjects
{
    [CreateAssetMenu(fileName = "CD_LocalAnomaly", menuName = "GraduationGame/CD_LocalAnomaly", order = 3)]
    public class CD_LocalAnomaly : ScriptableObject
    {
        public LocalAnomalyData localAnomalyData;
    }
}