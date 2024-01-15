using System;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Runtime.Controllers.UI
{
    public class LoadingPanelController : MonoBehaviour
    {
        [SerializeField] private GameObject startButton;
        [SerializeField] private GameObject loadingText;

        [Space] 
        [Header("Select Delay Duration Range")]
        [MinMaxSlider(1f, 10f)]
        [SerializeField] private Vector2 delayRange;

        [Space] 
        [Header("Current Delay Duration")]
        [ShowInInspector] private float _delayDuration;

        private void Awake()
        {
            _delayDuration = (int)Random.Range(delayRange.x, delayRange.y);
        }

        private void Start()
        {
            DOTween.Sequence()
                .AppendInterval(_delayDuration)
                .OnComplete(() => ActivateStartButton());
        }

        private void ActivateStartButton()
        {
            loadingText.SetActive(false);
            startButton.SetActive(true);
        }
    }
}