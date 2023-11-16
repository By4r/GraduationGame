using System;
using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Runtime.Controllers
{
    public class TestAnomaly : MonoBehaviour
    {
        [Button("Spawn Anomaly")]
        private void SpawnAnomaly()
        {
            AnomalySignals.Instance.onAnomalySpawn?.Invoke();
        }
    }
}