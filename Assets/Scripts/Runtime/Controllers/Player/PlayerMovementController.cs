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

        // [SerializeField] private AudioSource playerAudioSource;
        // [SerializeField] private AudioClip walkSound;
        [SerializeField] private UnityEngine.Camera playerCamera;
        [SerializeField] public  CharacterController characterController;
        [SerializeField] private StaminaController _staminaController;
        [SerializeField] private bool canRun;
        [SerializeField] public bool canMove =true;
        #endregion

        #region Private Variables
        
        #endregion
        
        #region ShowInInspector Variables
        [ShowInInspector] private PlayerMovementData _data;
        [ShowInInspector] private PlayerFOVData _fovData;
        #endregion
        
        #endregion
        
       

        internal void SetData(PlayerMovementData movementData)
        {
            _data = movementData;
        }
        internal void SetData(PlayerFOVData fovData)
        {
            _fovData = fovData;
        }

        private void Start()
        {
            if (CompareTag("MainCamera"))
            {
                playerCamera = FindObjectOfType<UnityEngine.Camera>();
            }
            characterController = GetComponent<CharacterController>();
            _staminaController = FindObjectOfType<StaminaController>();
        }
        private void FixedUpdate()
        {
            if (canMove)
            {
                RunOrSprint();
            }else StopPlayer();

            
        }


        private void MovePlayer()
        {
            canMove = true;
            Vector3 move = CalculateMoveVector();
            characterController.Move(move * _data.ForwardSpeed * Time.deltaTime);
            //playerAudioSource.PlayOneShot(walkSound);
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
                playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, _fovData.SprintFOV, Time.deltaTime * 2f);
            }
            else 
            {
                MovePlayer();
                Debug.Log("IncreaseStamina and move");
                playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, _fovData.NormalFOV, Time.deltaTime * 2f);
            }

            if (!shift)
            {
                canRun = true; 
            }

        }
        public void StopPlayer()
        {
            canMove=false;
            // rigidbody.velocity = Vector3.zero;
            // rigidbody.angularVelocity = Vector3.zero;
            characterController.Move(Vector3.zero);
            //characterController.transform.Rotate(Vector3.up * mouseX);
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
                    canRun = false; 
                }
            }
            
        }
    }
}