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

            // If anomalyHolder is null, return early
            if (anomalyHolder == null)
            {
                Debug.LogWarning("ANOMALY CONTROLLER HOLDER WARNING");

                return;
            }

            // If there is already an anomaly at this location, do not instantiate again
            if (anomalyHolder.Count > _currentHolder && anomalyHolder[_currentHolder] != null)
            {
                Debug.LogWarning("Anomaly already exists in the holder: " + anomalyHolder[_currentHolder].name);
                return;
            }

            // Instantiate a new anomaly at the specified index
            var newAnomaly = Instantiate(anomalyPrefab, anomalyHolder[_currentHolder]);

            Debug.LogWarning("Anomaly spawned and parented to the holder: " + newAnomaly.name);
        }
    }
}