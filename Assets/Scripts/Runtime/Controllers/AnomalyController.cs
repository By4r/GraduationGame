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

            if (anomalyHolder == null)
            {
                return;
            }

            var newAnomaly = Instantiate(anomalyPrefab, anomalyHolder[_currentHolder]);


            Debug.LogWarning("Anomaly spawned and parented to the holder: " + newAnomaly.name);
        }
        
    }
}