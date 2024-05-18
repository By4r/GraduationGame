using Runtime.Controllers.Player;
using Unity.Mathematics;
using UnityEngine;
using Runtime.Data.ValueObjects;
using Sirenix.OdinInspector;

namespace Runtime.Controllers.Camera
{
    public class CameraController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private float mouseSensitivity;
        [SerializeField] private PlayerMovementController playerMovementController;
        public bool mouseState = true;
        

        // [SerializeField] private Vector3 vectorOffset;
        // [SerializeField] private GameObject flashlight;
        // [SerializeField] private float speed;
        
        
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
            LockMouseCursor(); //Set Active !!!

        }

        private void LateUpdate()
        {
            if (mouseState)
            {
                UpdateMouseAxis();
            }
           
        }

        internal void LockMouseCursor()
        {
           Cursor.lockState = CursorLockMode.Locked;
           mouseState = true;
        }

        
        [Button ("UnlockMouseCursor")]
        public void UnlockMouseCursor()
        {
            Cursor.lockState = CursorLockMode.Confined;
            mouseState = false;

        }

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
      

        
    }
}
