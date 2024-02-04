using System;
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
        #region Serialized Variables

        [SerializeField] private AnomalyController anomalyController;

        #endregion

        #region Private Variables

        private LocalAnomalyData _localAnomalyData;

        private UniqueAnomalyData _uniqueAnomalyData;

        //[ShowInInspector] private int _currentAnomalyIndex;

        [SerializeField] private int _currentAnomalyIndex;


        //private AnomalyStageTypes _currentStage;

        [SerializeField] private AnomalyStageTypes _currentStage;

        [ShowInInspector] private int _reportedAnomalyValue;

        #endregion

        private void Awake()
        {
            _localAnomalyData = GetLocalAnomalyData();
            _uniqueAnomalyData = GetUniqueAnomalyData();
        }


        private LocalAnomalyData GetLocalAnomalyData()
        {
            return Resources.Load<CD_LocalAnomaly>("Data/CD_LocalAnomaly").localAnomalyData;
        }

        private UniqueAnomalyData GetUniqueAnomalyData()
        {
            return Resources.Load<CD_UniqueAnomaly>("Data/CD_UniqueAnomaly").uniqueAnomalyData;
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            AnomalySignals.Instance.onAnomalySpawn += OnAnomalySpawn;
            AnomalySignals.Instance.onAnomalyStage += OnAnomalyStage;
            AnomalySignals.Instance.onAnomalyReport += OnAnomalyReport;
            AnomalySignals.Instance.onCheckAnomalyResult += OnCheckAnomalyResult;
        }

        private void OnCheckAnomalyResult()
        {
            // Check if the reported anomaly value matches the total number of anomalies
            if (_reportedAnomalyValue == GetTotalAnomalyCount())
            {
                Debug.LogWarning("All anomalies reported!");
                // Perform any action you want when all anomalies are reported
                
                CoreGameSignals.Instance.onLevelSuccessful?.Invoke();
                
            }
            else
            {
                Debug.LogWarning("Not all anomalies reported yet. Continue investigating!");
                
                CoreGameSignals.Instance.onLevelFailed?.Invoke();

                // Perform any action for the case where not all anomalies are reported
            }
        }

        private int GetTotalAnomalyCount()
        {
            // Return the total number of anomalies (number of elements in anomalyHolder list)
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
            switch (_currentStage)
            {
                case AnomalyStageTypes.Part1:
                    anomalyController.SpawnAnomaly(_localAnomalyData.SpawnReferences[(int)AnomalyStageTypes.Part1],
                        _currentAnomalyIndex);
                    // anomalyController.SpawnAnomaly(_uniqueAnomalyData.SpawnReferences[(int)AnomalyStageTypes.Part1],
                    //     _currentAnomalyIndex);
                    break;

                case AnomalyStageTypes.Part2:
                    anomalyController.SpawnAnomaly(_localAnomalyData.SpawnReferences[(int)AnomalyStageTypes.Part2],
                        _currentAnomalyIndex);
                    // anomalyController.SpawnAnomaly(_uniqueAnomalyData.SpawnReferences[(int)AnomalyStageTypes.Part2],
                    //     _currentAnomalyIndex);
                    break;

                case AnomalyStageTypes.Part3:
                    anomalyController.SpawnAnomaly(_localAnomalyData.SpawnReferences[(int)AnomalyStageTypes.Part3],
                        _currentAnomalyIndex);
                    // anomalyController.SpawnAnomaly(_uniqueAnomalyData.SpawnReferences[(int)AnomalyStageTypes.Part3],
                    //     _currentAnomalyIndex);
                    break;

                case AnomalyStageTypes.Part4:
                    anomalyController.SpawnAnomaly(_localAnomalyData.SpawnReferences[(int)AnomalyStageTypes.Part4],
                        _currentAnomalyIndex);
                    // anomalyController.SpawnAnomaly(_uniqueAnomalyData.SpawnReferences[(int)AnomalyStageTypes.Part4],
                    //     _currentAnomalyIndex);
                    break;

                case AnomalyStageTypes.Part5:
                    anomalyController.SpawnAnomaly(_localAnomalyData.SpawnReferences[(int)AnomalyStageTypes.Part5],
                        _currentAnomalyIndex);
                    // anomalyController.SpawnAnomaly(_uniqueAnomalyData.SpawnReferences[(int)AnomalyStageTypes.Part5],
                    //     _currentAnomalyIndex);
                    break;

                case AnomalyStageTypes.Part6:
                    anomalyController.SpawnAnomaly(_localAnomalyData.SpawnReferences[(int)AnomalyStageTypes.Part6],
                        _currentAnomalyIndex);
                    Debug.LogWarning("LAST PART !!!!");
                    // anomalyController.SpawnAnomaly(_uniqueAnomalyData.SpawnReferences[(int)AnomalyStageTypes.Part6],
                    //     _currentAnomalyIndex);
                    break;
                //default:
                //throw new ArgumentOutOfRangeException();
            }

            ++_currentAnomalyIndex;
            
            Debug.Log("Anomaly Spawned !");
        }

        private void OnAnomalyStage(AnomalyStageTypes state)
        {
            _currentStage = state;
            Debug.LogWarning(_currentStage);
        }

        private void UnSubscribeEvents()
        {
            AnomalySignals.Instance.onAnomalySpawn -= OnAnomalySpawn;
            AnomalySignals.Instance.onAnomalyStage -= OnAnomalyStage;
            AnomalySignals.Instance.onAnomalyReport -= OnAnomalyReport;
            AnomalySignals.Instance.onCheckAnomalyResult -= OnCheckAnomalyResult;

        }


        private void OnDisable()
        {
            UnSubscribeEvents();
        }
        
    }
}