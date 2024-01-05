using System;
using TMPro;
using UnityEngine;

namespace Runtime.Controllers.UI
{
    public class StageAnomalyUIController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI stageText;

        private int _currentStage;

        private readonly int _maxStage = 5;

        private void Start()
        {
            ShowStage();
        }
        
        internal void UpdateStageIndex(int currentStage)
        {
            _currentStage = currentStage;
            ShowStage();
        }

        private void ShowStage()
        {
            stageText.text = "Stage: " + _currentStage + " / " + _maxStage + " (Total Anomaly " + (_maxStage + 1) + ")";
        }

    }
    
    
    
    
}