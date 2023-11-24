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
        
        [SerializeField] public CharacterController characterController;

        [SerializeField] private SprintStaminaController _staminaController;
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
            _staminaController = FindObjectOfType<SprintStaminaController>();
        }

        private void FixedUpdate()
        {
            RunOrSprint();
        }


        private void MovePlayer()
        {
            var x = Input.GetAxis("Horizontal");
           
            var z = Input.GetAxis("Vertical");
            
            Vector3 move = transform.right * x + transform.forward * z;
            characterController.Move(move * _data.ForwardSpeed * Time.deltaTime);
            
            _staminaController.IncreaseStamina();
        }

        private void RunOrSprint()
        {
            var shift = Input.GetKey(KeyCode.LeftShift);
            if (shift)
            {
                SprintPlayer();
                Debug.Log("DecreaseStamina and run");
            }
            else
            {
                MovePlayer();
                Debug.Log("IncreaseStamina and move");
            }

        }
        private void SprintPlayer()
        {
            if (Input.GetKey(KeyCode.LeftShift) && (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow)))
            {
                Vector3 run = transform.forward;
                characterController.Move(run * _data.SprintSpeed * Time.deltaTime);
                _staminaController.DecreaseStamina();
            }
        }
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            PlayerSignals.Instance.onMovePlayer += MovePlayer;
            PlayerSignals.Instance.onRunPlayer += SprintPlayer;
            PlayerSignals.Instance.onRunOrSprint += RunOrSprint;
        }
        private void OnDisable()
        {
            UnSubscribeEvents();
        }
        private void UnSubscribeEvents()
        {
            PlayerSignals.Instance.onMovePlayer -= MovePlayer;
            PlayerSignals.Instance.onRunPlayer -= SprintPlayer;
            PlayerSignals.Instance.onRunOrSprint -= RunOrSprint;
        }
    }
}