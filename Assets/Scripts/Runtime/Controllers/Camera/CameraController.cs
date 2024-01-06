using System;
using Runtime.Controllers.Player;
using Unity.Mathematics;
using UnityEngine;
using Runtime.Data.ValueObjects;
using Runtime.Keys;
using UnityEngine.Rendering;

namespace Runtime.Controllers.Camera
{
    public class CameraController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private float mouseSensitivity;
        [SerializeField] private PlayerMovementController _playerMovementController;

        #endregion

        #region Private Variables

        private PlayerMovementData _data;
        private float xRotation;
        private float2 _clampValues;

        #endregion

        #endregion

        private void Start()
        {
            RemoveMouseCursor();
        }

        private void LateUpdate()
        {
            MoveMouseAxis();
        }

        private void RemoveMouseCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void MoveMouseAxis()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            _playerMovementController.characterController.transform.Rotate(Vector3.up * mouseX);
        }
        
    }
}