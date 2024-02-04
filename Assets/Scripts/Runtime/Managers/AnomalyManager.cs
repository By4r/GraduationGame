using System;
using System.Collections.Generic;
using Runtime.Controllers;
using Runtime.Data.ValueObjects;
using Runtime.Enums;
using Runtime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Runtime.Managers
{
    public class AnomalyManager : MonoBehaviour
    {
        [SerializeField] private AnomalyController anomalyController;
        private LocalAnomalyData _localAnomalyData;
        private UniqueAnomalyData _uniqueAnomalyData;
        [SerializeField] private int _currentAnomalyIndex;
        [SerializeField] private AnomalyStageTypes _currentStage;
        [ShowInInspector] private int _reportedAnomalyValue;

        private List<AnomalyStageTypes> availableStages = new List<AnomalyStageTypes>();

        private void Awake()
        {
            _localAnomalyData = GetLocalAnomalyData();
            _uniqueAnomalyData = GetUniqueAnomalyData();
            InitializeAvailableStages();
            UpdateCurrentStage();
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void SubscribeEvents()
        {
            AnomalySignals.Instance.onAnomalySpawn += OnAnomalySpawn;
            AnomalySignals.Instance.onAnomalyStage += OnAnomalyStage;
            AnomalySignals.Instance.onAnomalyReport += OnAnomalyReport;
            AnomalySignals.Instance.onCheckAnomalyResult += OnCheckAnomalyResult;
        }

        private void UnSubscribeEvents()
        {
            AnomalySignals.Instance.onAnomalySpawn -= OnAnomalySpawn;
            AnomalySignals.Instance.onAnomalyStage -= OnAnomalyStage;
            AnomalySignals.Instance.onAnomalyReport -= OnAnomalyReport;
            AnomalySignals.Instance.onCheckAnomalyResult -= OnCheckAnomalyResult;
        }

        private void InitializeAvailableStages()
        {
            // Initialize the list with all possible stages
            Array values = Enum.GetValues(typeof(AnomalyStageTypes));
            foreach (AnomalyStageTypes stage in values)
            {
                availableStages.Add(stage);
            }
        }

        private void UpdateCurrentStage()
        {
            // Ensure there are available stages to choose from
            if (availableStages.Count > 0)
            {
                // Select a random index from the available stages
                int randomIndex = UnityEngine.Random.Range(0, availableStages.Count);

                // Set the current stage and remove it from the available stages
                _currentStage = availableStages[randomIndex];
                availableStages.RemoveAt(randomIndex);
            }
            else
            {
                // If there are no available stages, handle it based on your requirements
                Debug.LogWarning("No more available stages!");
            }
        }

        private void OnCheckAnomalyResult()
        {
            if (_reportedAnomalyValue == GetTotalAnomalyCount())
            {
                Debug.LogWarning("All anomalies reported!");
                CoreGameSignals.Instance.onLevelSuccessful?.Invoke();
            }
            else
            {
                Debug.LogWarning("Not all anomalies reported yet. Continue investigating!");
                CoreGameSignals.Instance.onLevelFailed?.Invoke();
            }
        }

        private int GetTotalAnomalyCount()
        {
            return anomalyController.GetAnomalyCount();
        }

        private void OnAnomalyReport()
        {
            _reportedAnomalyValue++;
            Debug.LogWarning("REPORTED ANOMALY VALUE INCREASED");
            Debug.LogWarning("REPORTED ANOMALY VALUE :" + _reportedAnomalyValue);
        }

        private void OnAnomalySpawn()
        {
            // Spawn anomaly based on the current stage
            switch (_currentStage)
            {
                case AnomalyStageTypes.Part1:
                    anomalyController.SpawnAnomaly(_localAnomalyData.SpawnReferences[(int)AnomalyStageTypes.Part1],
                        _currentAnomalyIndex);
                    break;

                case AnomalyStageTypes.Part2:
                    anomalyController.SpawnAnomaly(_localAnomalyData.SpawnReferences[(int)AnomalyStageTypes.Part2],
                        _currentAnomalyIndex);
                    break;

                case AnomalyStageTypes.Part3:
                    anomalyController.SpawnAnomaly(_localAnomalyData.SpawnReferences[(int)AnomalyStageTypes.Part3],
                        _currentAnomalyIndex);
                    break;

                case AnomalyStageTypes.Part4:
                    anomalyController.SpawnAnomaly(_localAnomalyData.SpawnReferences[(int)AnomalyStageTypes.Part4],
                        _currentAnomalyIndex);
                    break;

                case AnomalyStageTypes.Part5:
                    anomalyController.SpawnAnomaly(_localAnomalyData.SpawnReferences[(int)AnomalyStageTypes.Part5],
                        _currentAnomalyIndex);
                    break;

                case AnomalyStageTypes.Part6:
                    anomalyController.SpawnAnomaly(_localAnomalyData.SpawnReferences[(int)AnomalyStageTypes.Part6],
                        _currentAnomalyIndex);
                    Debug.LogWarning("LAST PART !!!!");
                    break;
            }

            _currentAnomalyIndex++;
            Debug.Log("Anomaly Spawned !");
            UpdateCurrentStage(); // Update stage after spawning
        }

        private void OnAnomalyStage(AnomalyStageTypes state)
        {
            // Handle the event of anomaly stage change if needed
            // You may not need to do anything here based on your implementation
        }

        private LocalAnomalyData GetLocalAnomalyData()
        {
            return Resources.Load<CD_LocalAnomaly>("Data/CD_LocalAnomaly").localAnomalyData;
        }

        private UniqueAnomalyData GetUniqueAnomalyData()
        {
            return Resources.Load<CD_UniqueAnomaly>("Data/CD_UniqueAnomaly").uniqueAnomalyData;
        }
    }
}
