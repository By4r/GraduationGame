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

        [ShowInInspector] private int _currentAnomalyIndex;

        //private AnomalyStageTypes _currentStage;

        [SerializeField] private AnomalyStageTypes _currentStage;

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
                    // anomalyController.SpawnAnomaly(_uniqueAnomalyData.SpawnReferences[(int)AnomalyStageTypes.Part6],
                    //     _currentAnomalyIndex);
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