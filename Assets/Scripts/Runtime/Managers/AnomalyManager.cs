using Runtime.Controllers;
using Runtime.Data.ValueObjects;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class AnomalyManager : MonoBehaviour
    {
        #region Serialized Variables

        [SerializeField] private AnomalyController anomalyController;

        #endregion

        #region Private Variables

        private AnomalyData _anomalyData;

        #endregion

        private void Awake()
        {
            _anomalyData = GetAnomalyData();
        }


        private AnomalyData GetAnomalyData()
        {
            return Resources.Load<CD_Anomaly>("Data/CD_Anomaly").anomalyData;
        }


        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            AnomalySignals.Instance.onAnomalySpawn += OnAnomalySpawn;
        }

        private void OnAnomalySpawn()
        {
            Debug.LogWarning("Anomaly Spawned!");
        }

        private void UnSubscribeEvents()
        {
            AnomalySignals.Instance.onAnomalySpawn -= OnAnomalySpawn;
        }


        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}