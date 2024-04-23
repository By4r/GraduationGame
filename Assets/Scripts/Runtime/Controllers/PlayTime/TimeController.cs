using System;
using System.Collections;
using Runtime.Controllers.UI;
using Runtime.Enums;
using Runtime.Signals;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Runtime.Controllers.PlayTime
{
    public class TimeController : MonoBehaviour
    {
        #region TimeController Variables

        [SerializeField] private float totalTime; // 30 minutes in seconds // 1800f
        private float currentTime;
        private bool isCounting = false;
        [SerializeField] private TextMeshProUGUI timerText;

        [SerializeField]
        private StageAnomalyUIController stageAnomalyUIController; // Should Remove AFTER TEST!


        private float uniqueAnomalyTime; // Uniquely generated anomaly time
        private float localAnomalyTime; // Anomaly time local to the current stage

        private int currentStage = 0; // Initial stage

        private float _stageIncrementDuration;

        private float stageTime;
        
        
        private bool hasEnteredLocalAnomaly = false;
        private bool hasEnteredUniqueAnomaly = false;
        
        #endregion

        private void Awake()
        {
            if (totalTime != 0)
            {
                _stageIncrementDuration = totalTime / 10f;
                
                Debug.LogWarning("STAGE INCREMENT DURATION " + _stageIncrementDuration);
            }
            else
            {
                // Handle the case where initialTime is 0 to avoid division by zero.
                Debug.LogError("Total time cannot be zero.");
            }
        }

        void Start()
        {
            currentTime = 0f;
            stageTime = 0f;
            UpdateTimerText();
            RandomAnomalyTime();
            stageAnomalyUIController.UpdateStageIndex(currentStage);
        }

        void Update()
        {
            Debug.LogWarning(totalTime);
            if (isCounting)
            {
                currentTime += Time.deltaTime;
                stageTime += Time.deltaTime;

                if (currentTime >= totalTime)
                {
                    currentTime = 0f;
                    isCounting = false;
                    ShowTimeUpMessage();

                    CheckGameResult();
                }

                if (stageTime >= _stageIncrementDuration)
                {
                    ResetStageTime();
                    IncrementStage();
                    RandomAnomalyTime();
                }

                UpdateTimerText();

                if (stageTime >= localAnomalyTime && !hasEnteredLocalAnomaly)
                {
                    Debug.Log("Local Anomaly Time in");
                    
                    AnomalySignals.Instance.onAnomalySpawn?.Invoke();
                    hasEnteredLocalAnomaly = true; // Set the flag to true to prevent further logs
                }

                if (stageTime >= uniqueAnomalyTime && !hasEnteredUniqueAnomaly)
                {
                    Debug.Log("Unique Anomaly Time in");
                    hasEnteredUniqueAnomaly = true; // Set the flag to true to prevent further logs
                }
                
                // Reset hasEnteredLocalAnomaly flag if stageTime is less than localAnomalyTime
                if (stageTime < localAnomalyTime)
                {
                    hasEnteredLocalAnomaly = false;
                }

                // if (Mathf.Approximately((totalTime - currentTime) % _stageIncrementDuration, 0f))
                // {
                //     IncrementStage();
                //     RandomAnomalyTime();
                //     //ResetStageTime();
                // }
            }
        }

        private void CheckGameResult()
        {
            Debug.LogWarning("Checking Result!");
            
            AnomalySignals.Instance.onCheckAnomalyResult?.Invoke();
            
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

        private void RandomAnomalyTime()
        {
            // Choose a random duration within the _stageIncrementDuration range
            uniqueAnomalyTime = Random.Range(0f, _stageIncrementDuration);

            // Choose a random duration for local use within the _stageIncrementDuration range
            localAnomalyTime = Random.Range(0f, _stageIncrementDuration);

            // If the localAnomalyTime has been used before, select a new one 
            while (uniqueAnomalyTime == localAnomalyTime)
            {
                localAnomalyTime = Random.Range(0f, _stageIncrementDuration);
            }

            // Output the selected times for debugging purposes
            Debug.Log("Local Anomaly Time: " + localAnomalyTime);
            Debug.Log("Unique Anomaly Time: " + uniqueAnomalyTime);
        }


        private void IncrementStage()
        {
            // Increment the stage
            currentStage++;
            Debug.LogWarning("CURRENT STAGE INT:  " + currentStage);
            
            stageAnomalyUIController.UpdateStageIndex(currentStage); // Should Remove After Test
            
            // Ensure the stage doesn't go beyond the maximum value
            if (currentStage > (int)AnomalyStageTypes.Part6)
            {
                currentStage = (int)AnomalyStageTypes.Part6;
            }
            
            // Notify about the new stage
            AnomalySignals.Instance.onAnomalyStage?.Invoke((AnomalyStageTypes)currentStage);
            Debug.Log("INCREMENT STAGE");
        }
        
        internal void StartTimeWithDelay(float delayValue)
        {
            StartCoroutine(StartTimerWithDelayCoroutine(delayValue));
        }

        private IEnumerator StartTimerWithDelayCoroutine(float delayDuration)
        {
            yield return new WaitForSeconds(delayDuration);

            isCounting = true;
        }
        
        private void ResetStageTime()
        {
            stageTime = 0f;
        }
    }
}