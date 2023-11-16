using System;
using Runtime.Enums;
using Runtime.Signals;
using TMPro;
using UnityEngine;

namespace Runtime.Controllers.PlayTime
{
    public class TimeController : MonoBehaviour
    {
        #region TimeController Variables

        [SerializeField] private float initialTime = 1200f; // 20 minutes in seconds
        private float currentTime;
        private bool isCounting = false;
        [SerializeField] private TextMeshProUGUI timerText;

        private int currentStage = 1; // Initial stage

        private float _stageIncrementDuration;

        #endregion

        private void Awake()
        {
            if (initialTime != 0)
            {
                _stageIncrementDuration = initialTime / 5f;
            }
            else
            {
                // Handle the case where initialTime is 0 to avoid division by zero.
                Debug.LogError("Initial time cannot be zero.");
            }
        }

        void Start()
        {
            currentTime = initialTime;
            UpdateTimerText();
        }

        void Update()
        {
            if (isCounting)
            {
                currentTime -= Time.deltaTime;

                if (currentTime <= 0f)
                {
                    currentTime = 0f;
                    isCounting = false;
                    ShowTimeUpMessage();
                }

                UpdateTimerText();


                if (Mathf.Approximately((initialTime - currentTime) % _stageIncrementDuration, 0f))
                {
                    IncrementStage();
                }
            }
        }

        internal void StartTimer()
        {
            isCounting = true;
        }

        private void UpdateTimerText()
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(currentTime);
            timerText.text = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
        }

        private void ShowTimeUpMessage()
        {
            Debug.Log("Time's up!");
        }

        private void IncrementStage()
        {
            // Increment the stage
            currentStage++;

            // Ensure the stage doesn't go beyond the maximum value
            if (currentStage > (int)AnomalyStageTypes.Part5)
            {
                currentStage = (int)AnomalyStageTypes.Part5;
            }

            // Notify about the new stage
            AnomalySignals.Instance.onAnomalyStage?.Invoke((AnomalyStageTypes)currentStage);
        }
    }
}