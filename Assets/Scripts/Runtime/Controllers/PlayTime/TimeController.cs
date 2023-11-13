﻿using System;
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

        #endregion

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
    }
}