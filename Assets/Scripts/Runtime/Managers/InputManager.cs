using System.Collections.Generic;
using Runtime.Data.UnityObjects;
using Runtime.Data.ValueObjects;
using Runtime.Enums;
//using Runtime.Keys;
using Runtime.Signals;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.Managers
{
    public class InputManager : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        [ShowInInspector] private InputData _data;
        [ShowInInspector] private bool _isAvailableForTouch, _isFirstTimeTouchTaken, _isTouching, _isInventoryPanelOpen;

        private float _currentVelocity;
        private float3 _moveVector;
        private Vector2? _mousePosition;

        #endregion

        #endregion

        private void Awake()
        {
            _data = GetInputData();
        }

        private InputData GetInputData()
        {
            return Resources.Load<CD_Input>("Data/CD_Input").Data;
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onReset += OnReset;
            InputSignals.Instance.onEnableInput += OnEnableInput;
            InputSignals.Instance.onDisableInput += OnDisableInput;
        }

        private void OnDisableInput()
        {
            _isAvailableForTouch = false;
        }

        private void OnEnableInput()
        {
            _isAvailableForTouch = true;
        }

        private void OnReset()
        {
            _isAvailableForTouch = false;
            //_isFirstTimeTouchTaken = false;
            _isTouching = false;
        }

        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onReset -= OnReset;
            InputSignals.Instance.onEnableInput -= OnEnableInput;
            InputSignals.Instance.onDisableInput -= OnDisableInput;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                TimeSignals.Instance.onTimeStarted?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (_isInventoryPanelOpen)
                {
                    // If the inventory panel is open, close it
                    CoreUISignals.Instance.onClosePanel?.Invoke(5);
                }
                else
                {
                    // If the inventory panel is closed, open it
                    CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Inventory, 5); // 5 is optional parameter
                }
            }
        }
    }
}