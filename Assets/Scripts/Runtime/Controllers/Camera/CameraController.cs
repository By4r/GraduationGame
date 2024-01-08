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
        public bool mouseState = true;

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
            if (mouseState)
            {
                UpdateMouseAxis();
            }
            //else FreezeMouseAxis();
        }

        internal void RemoveMouseCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        internal void EnableMouseCursor()
        {
            Cursor.lockState = CursorLockMode.Confined;
        }

        // private void MoveMouseAxis()
        // {
        //     
        //     float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        //     float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        //
        //     xRotation -= mouseY;
        //     xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        //
        //     transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        //
        //     _playerMovementController.characterController.transform.Rotate(Vector3.up * mouseX);
        // }

        //uçak oldu amk
        // private void UpdateMouseAxis()
        // {
        //     float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        //     float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        //
        //     xRotation -= mouseY;
        //     xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        //
        //     transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        //
        //     Vector3 rotationAmount = mouseState ? new Vector3(-mouseY, mouseX, 0f) : Vector3.zero;
        //
        //     _playerMovementController.characterController.transform.Rotate(rotationAmount);
        // }
        private void UpdateMouseAxis()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            if (mouseState)
            {
                _playerMovementController.characterController.transform.Rotate(Vector3.up * mouseX);
            }
            else
            {
                _playerMovementController.characterController.transform.rotation = Quaternion.identity;
            }
        }
    }
}