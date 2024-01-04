using System;
using Runtime.Controllers.Stamina;
using Runtime.Data.ValueObjects;
using Runtime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        
        [SerializeField] public  CharacterController characterController;

        [SerializeField] private StaminaController _staminaController;
        [SerializeField] private bool canRun;
        #endregion

        #region Private Variables

        [ShowInInspector] private PlayerMovementData _data;

        #endregion

        #endregion

        internal void SetData(PlayerMovementData data)
        {
            _data = data;
        }

        private void Start()
        {
            characterController = GetComponent<CharacterController>();
            _staminaController = FindObjectOfType<StaminaController>();
        }
        private void FixedUpdate()
        {
            RunOrSprint();
        }


        private void MovePlayer()
        {
            Vector3 move = CalculateMoveVector();
            characterController.Move(move * _data.ForwardSpeed * Time.deltaTime);

            if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
            {
                _staminaController.IncreaseStamina();
            }
        }
        private bool CanSprint()
        {
            return Input.GetKey(KeyCode.LeftShift) && (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow)) && canRun;
        }

        private Vector3 CalculateMoveVector()
        {
            var x = Input.GetAxisRaw("Horizontal");
            var z = Input.GetAxisRaw("Vertical");
            return transform.right * x + transform.forward * z;
        }

        private void RunOrSprint()
        {
            var shift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
            var w = Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow);
            

            if (shift && w && canRun )
            {
                SprintPlayer();
                Debug.Log("DecreaseStamina and run");
            }
            else 
            {
                MovePlayer();
                Debug.Log("IncreaseStamina and move");
            }

            if (!shift)
            {
                canRun = true; // Reset canRun when shift key is released
            }

        }
        private void SprintPlayer()
        {
            if (CanSprint())
            {
                Vector3 move = CalculateMoveVector();
                characterController.Move(move * _data.SprintSpeed * Time.deltaTime);
                _staminaController.DecreaseStamina();

                if (_staminaController.sprintStamina <= 0.01f)
                {
                    canRun = false; // Disable running when stamina is depleted
                }
            }
            
        }
    }
}