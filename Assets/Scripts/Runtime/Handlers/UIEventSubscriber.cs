using System.Runtime.Remoting.Messaging;
using Runtime.Enums;
using Runtime.Managers;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Handlers
{
    public class UIEventSubscriber : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private UIEventSubscriptionTypes type;
        [SerializeField] private Button button;

        #endregion

        #region Private Variables

        [ShowInInspector] private UIManager _manager;

        #endregion

        #endregion

        private void Awake()
        {
            FindReferences();
        }

        private void FindReferences()
        {
            _manager = FindObjectOfType<UIManager>();
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            switch (type)
            {
                case UIEventSubscriptionTypes.OnPlay:
                {
                    button.onClick.AddListener(_manager.Play);
                    break;
                }
                case UIEventSubscriptionTypes.OnNextLevel:
                {
                    button.onClick.AddListener(_manager.NextLevel);
                    break;
                }
                case UIEventSubscriptionTypes.OnRestartLevel:
                {
                    button.onClick.AddListener(_manager.RestartLevel);
                    break;
                }
                case UIEventSubscriptionTypes.OnSettings:
                {
                    button.onClick.AddListener(_manager.Settings);
                    break;
                }
                case UIEventSubscriptionTypes.OnResumeGame:
                {
                    button.onClick.AddListener(_manager.Resume);
                    break;
                }
                case UIEventSubscriptionTypes.OnExitSettings:
                {
                    button.onClick.AddListener(_manager.CloseSettings);
                    break;
                }
                case UIEventSubscriptionTypes.OnMainMenu:
                {
                    button.onClick.AddListener(_manager.ReturnMainMenu);
                    break;
                }
                case UIEventSubscriptionTypes.OnStart:
                {
                    button.onClick.AddListener(_manager.LoadGame);
                    break;
                }
            }
        }

        private void UnsubscribeEvents()
        {
            switch (type)
            {
                case UIEventSubscriptionTypes.OnPlay:
                {
                    button.onClick.RemoveListener(_manager.Play);
                    break;
                }
                case UIEventSubscriptionTypes.OnNextLevel:
                {
                    button.onClick.RemoveListener(_manager.NextLevel);
                    break;
                }
                case UIEventSubscriptionTypes.OnRestartLevel:
                {
                    button.onClick.RemoveListener(_manager.RestartLevel);
                    break;
                }
                case UIEventSubscriptionTypes.OnSettings:
                {
                    button.onClick.RemoveListener(_manager.Settings);
                    break;
                }
                case UIEventSubscriptionTypes.OnResumeGame:
                {
                    button.onClick.RemoveListener(_manager.Resume);
                    break;
                }
                case UIEventSubscriptionTypes.OnExitSettings:
                {
                    button.onClick.RemoveListener(_manager.CloseSettings);
                    break;
                }
                case UIEventSubscriptionTypes.OnMainMenu:
                {
                    button.onClick.RemoveListener(_manager.ReturnMainMenu);
                    break;
                }
                case UIEventSubscriptionTypes.OnStart:
                {
                    button.onClick.RemoveListener(_manager.LoadGame);
                    break;
                }
            }
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
    }
}