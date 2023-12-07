using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Runtime.Data.ValueObjects
{
    [CreateAssetMenu(fileName = "CD_UniqueAnomaly", menuName = "GraduationGame/CD_UniqueAnomaly", order = 4)]
    public class CD_UniqueAnomaly : ScriptableObject
    {
        public UniqueAnomalyData uniqueAnomalyData;
    }
}