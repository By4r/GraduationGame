
using System;
using Runtime.Controllers.Camera;
using Runtime.Signals;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

namespace Runtime.Managers
{
    public class CameraManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [ShowInInspector] private CameraController cameraController;
        
        #endregion

        #region Private Variables

        private float3 _firstPosition;

        #endregion

        #endregion


        private void Awake()
        {
            cameraController = FindObjectOfType<CameraController>();
        }

        private void Start()
        {
            cameraController = FindObjectOfType<CameraController>();

            Init();
        }

        private void Init()
        {
            _firstPosition = transform.position;
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            
            CameraSignals.Instance.onSetCameraTarget += OnSetCameraTarget;
            CameraSignals.Instance.onCameraLocked += OnHideCursor;
            CameraSignals.Instance.onCameraConfine += OnEnableCursor;
            CoreGameSignals.Instance.onReset += OnReset;
        }

        private void OnSetCameraTarget()
        {
            var player = FindObjectOfType<PlayerManager>().transform;
            // virtualCamera.Follow = player;
            //virtualCamera.LookAt = player;
        }

        private void OnReset()
        {
            transform.position = _firstPosition;
        }

        private void UnSubscribeEvents()
        {
            CameraSignals.Instance.onSetCameraTarget -= OnSetCameraTarget;
            CameraSignals.Instance.onCameraLocked -= OnHideCursor;
            CameraSignals.Instance.onCameraConfine -= OnEnableCursor;
            CoreGameSignals.Instance.onReset -= OnReset;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void OnHideCursor()
        {
            Debug.LogWarning("HIDE WORKED !");
            cameraController.RemoveMouseCursor();
        }

        private void OnEnableCursor()
        {
            cameraController.EnableMouseCursor();
        }
    }
}