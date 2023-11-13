using System;
using Runtime.Controllers.PlayTime;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class TimeManager : MonoBehaviour
    {
        #region TimeManager Variables

        #region Serialized Variables

        [SerializeField] private TimeController timeController;

        #endregion

        #endregion

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            TimeSignals.Instance.onTimeStarted += OnTimeStarted;
        }

        private void OnTimeStarted()
        {
            timeController.StartTimer();
        }

        private void UnSubscribeEvents()
        {
            TimeSignals.Instance.onTimeStarted -= OnTimeStarted;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}