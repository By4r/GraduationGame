using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Controllers
{
    public class AnomalyController : MonoBehaviour
    {
        #region Serialized Variables

        [SerializeField] private List<Transform> anomalyHolder;

        #endregion

        #region Private

        private int _currentHolder;

        #endregion
        
        internal void SpawnAnomaly(GameObject anomalyPrefab, int currentHolder)
        {
            _currentHolder = currentHolder;

            Debug.LogWarning("Current Holder: " + _currentHolder);

            // If anomalyHolder is null or _currentHolder is out of range, return early
            if (anomalyHolder == null || _currentHolder < 0 || _currentHolder >= anomalyHolder.Count)
            {
                Debug.LogWarning("ANOMALY CONTROLLER HOLDER WARNING");
                return;
            }

            Transform holderTransform = anomalyHolder[_currentHolder];

            // If there is already a child (anomaly) under this holder, do not instantiate again
            if (holderTransform.childCount > 0)
            {
                Debug.LogWarning("Anomaly already exists in the holder: " + holderTransform.GetChild(0).name);
                return;
            }

            // Instantiate a new anomaly under the specified holder
            var newAnomaly = Instantiate(anomalyPrefab, holderTransform);

            Debug.LogWarning("Anomaly spawned and parented to the holder: " + newAnomaly.name);
        }
        
        internal int GetAnomalyCount()
        {
            // Return the total number of anomalies (number of elements in anomalyHolder list)
            return anomalyHolder.Count;
        }
        
    }
}