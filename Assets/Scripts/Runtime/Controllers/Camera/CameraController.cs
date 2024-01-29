using Runtime.Controllers.Player;
using Unity.Mathematics;
using UnityEngine;
using Runtime.Data.ValueObjects;

namespace Runtime.Controllers.Camera
{
    public class CameraController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private float mouseSensitivity;
        [SerializeField] private PlayerMovementController playerMovementController;
        public bool mouseState = true;
        [SerializeField] private float smoothMouseSpeed;
        
        #endregion

        
        #region Private Variables

        private PlayerMovementData _data;
        private float _xRotation;
        private float2 _clampValues;
        private Vector2 _smoothMouseInput;
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
                UpdateSmoothMouseAxis();
            }
            //else FreezeMouseAxis();
        }

        internal void RemoveMouseCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            //mouseState = true;
        }

        internal void EnableMouseCursor()
        {
            Cursor.lockState = CursorLockMode.Confined;
            //Cursor.lockState = CursorLockMode.None;
            //mouseState = false;

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

            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);

            if (mouseState)
            {
                playerMovementController.characterController.transform.Rotate(Vector3.up * mouseX);
            }
            else
            {
                playerMovementController.characterController.transform.rotation = Quaternion.identity;
            }
        }
        private void UpdateSmoothMouseAxis()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            _smoothMouseInput.x = Mathf.Lerp(_smoothMouseInput.x, mouseX, 1f / smoothMouseSpeed);
            _smoothMouseInput.y = Mathf.Lerp(_smoothMouseInput.y, mouseY, 1f / smoothMouseSpeed);

            _xRotation -= _smoothMouseInput.y;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);

            if (mouseState)
            {
                playerMovementController.characterController.transform.Rotate(Vector3.up * _smoothMouseInput.x);
            }
            else
            {
                playerMovementController.characterController.transform.rotation = Quaternion.identity;
            }
        }
        
    }
}