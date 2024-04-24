﻿using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Runtime.Signals;

namespace Runtime.Controllers
{
    public class SleepController : MonoBehaviour
    {
        [SerializeField] private GameObject sleepPanel;

        private Image sleepImage;

        private void Start()
        {
            sleepImage = sleepPanel.GetComponent<Image>();
        }

        internal void Sleep()
        {
            sleepPanel.SetActive(true);

            sleepImage.color = new Color(sleepImage.color.r, sleepImage.color.g, sleepImage.color.b, 0f);

            sleepImage.DOFade(1f, 1f)
                .SetEase(Ease.InOutQuad)
                .OnComplete(() =>
                {
                    DOVirtual.DelayedCall(1f, () =>
                    {
                        sleepImage.DOFade(0f, 1f)
                            .SetEase(Ease.InOutQuad);
                        
                        TaskSignals.Instance.onSleepDone?.Invoke(true);
                    });
                });
        }
    }
}