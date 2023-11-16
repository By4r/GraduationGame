using System;
using Runtime.Controllers;
using Runtime.Data.ValueObjects;
using Runtime.Enums;
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

        private int _currentAnomalyIndex;

        private AnomalyStageTypes _currentStage;

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
            AnomalySignals.Instance.onAnomalyStage += OnAnomalyStage;
        }


        private void OnAnomalySpawn()
        {
            switch (_currentStage)
            {
                case AnomalyStageTypes.Part1:
                    anomalyController.SpawnAnomaly(_anomalyData.SpawnReferences[(int)AnomalyStageTypes.Part1],
                        _currentAnomalyIndex);
                    break;

                case AnomalyStageTypes.Part2:
                    anomalyController.SpawnAnomaly(_anomalyData.SpawnReferences[(int)AnomalyStageTypes.Part2],
                        _currentAnomalyIndex);
                    break;

                case AnomalyStageTypes.Part3:
                    anomalyController.SpawnAnomaly(_anomalyData.SpawnReferences[(int)AnomalyStageTypes.Part3],
                        _currentAnomalyIndex);
                    break;

                case AnomalyStageTypes.Part4:
                    anomalyController.SpawnAnomaly(_anomalyData.SpawnReferences[(int)AnomalyStageTypes.Part4],
                        _currentAnomalyIndex);
                    break;

                case AnomalyStageTypes.Part5:
                    anomalyController.SpawnAnomaly(_anomalyData.SpawnReferences[(int)AnomalyStageTypes.Part5],
                        _currentAnomalyIndex);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Debug.Log("Anomaly Spawned !");
        }

        private void OnAnomalyStage(AnomalyStageTypes state)
        {
            _currentStage = state;
        }

        private void UnSubscribeEvents()
        {
            AnomalySignals.Instance.onAnomalySpawn -= OnAnomalySpawn;
            AnomalySignals.Instance.onAnomalyStage -= OnAnomalyStage;
        }


        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}