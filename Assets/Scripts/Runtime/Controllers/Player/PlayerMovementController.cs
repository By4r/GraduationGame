// using System;
// using Runtime.Controllers.Stamina;
// using Runtime.Data.ValueObjects;
// using Runtime.Signals;
// using Sirenix.OdinInspector;
// using UnityEngine;
//
//
// namespace Runtime.Controllers.Player
// {
//     public class PlayerMovementController : MonoBehaviour
//     {
//         #region Self Variables
//
//         #region Serialized Variables
//
//         // [SerializeField] private AudioSource playerAudioSource;
//         // [SerializeField] private AudioClip walkSound;
//         [SerializeField] private UnityEngine.Camera playerCamera;
//         [SerializeField] public  CharacterController characterController;
//         [SerializeField] private StaminaController _staminaController;
//         [SerializeField] private bool canRun;
//         [SerializeField] public bool canMove =true;
//         #endregion
//         
//         #region ShowInInspector Variables
//         [ShowInInspector] private PlayerMovementData _data;
//         [ShowInInspector] private PlayerFOVData _fovData;
//         [ShowInInspector] private float gravity = -9.81f;
//         [ShowInInspector] private Vector3 velocity;
//
//         #endregion
//         
//         #endregion
//         
//        
//
//         internal void SetData(PlayerMovementData movementData)
//         {
//             _data = movementData;
//         }
//         internal void SetData(PlayerFOVData fovData)
//         {
//             _fovData = fovData;
//         }
//
//         private void Start()
//         {
//             characterController = GetComponent<CharacterController>();
//             //_staminaController = FindObjectOfType<StaminaController>();
//         }
//         
//         private void FixedUpdate()
//         {
//             if (canMove)
//             {
//                 RunOrSprint();
//                 ApplyGravity();
//             }
//             else
//             {
//                 StopPlayer();
//                 ApplyGravity();
//             }
//             
//            
//         }
//         
//         private void ApplyGravity()
//         {
//             if (characterController.isGrounded && velocity.y < 0)
//             {
//                 velocity.y = -2f; // Zeminde olduğumuzda düşme hızını sıfırla
//             }
//
//             else
//             {
//                 velocity.y += gravity * _data.ForwardSpeed * Time.deltaTime; // Yer çekimini uygula
//                 characterController.Move(velocity * Time.deltaTime); // Yer çekimi ile hareket ettir
//             }
//         }
//
//         
//         private void MovePlayer()
//         {
//             canMove = true;
//             Vector3 move = CalculateMoveVector();
//             characterController.Move(move * _data.ForwardSpeed * Time.deltaTime);
//             
//             if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
//             {
//                 _staminaController.IncreaseStamina();
//             }
//         }
//         private bool CanSprint()
//         {
//             return Input.GetKey(KeyCode.LeftShift) && (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow)) && canRun;
//         }
//
//         private Vector3 CalculateMoveVector()
//         {
//             var x = Input.GetAxisRaw("Horizontal");
//             var z = Input.GetAxisRaw("Vertical");
//             return transform.right * x + transform.forward * z;
//         }
//
//         private void RunOrSprint()
//         {
//             var shift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
//             var w = Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow);
//             
//         
//             if (shift && w && canRun )
//             {
//                 SprintPlayer();
//                 Debug.Log("DecreaseStamina and run");
//                 playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, _fovData.SprintFOV, Time.deltaTime * 2f);
//             }
//             else 
//             {
//                 MovePlayer();
//                 Debug.Log("IncreaseStamina and move");
//                 playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, _fovData.NormalFOV, Time.deltaTime * 2f);
//             }
//             
//             if (!shift)
//             {
//                 canRun = true; 
//             }
//             
//         }
//
//         private void StopPlayer()
//         {
//             canMove=false;
//             characterController.Move(Vector3.zero);
//             
//         }
//         
//         private void SprintPlayer()
//         {
//             if (CanSprint())
//             {
//                 Vector3 move = CalculateMoveVector();
//                 characterController.Move(move * _data.SprintSpeed * Time.deltaTime);
//                 _staminaController.DecreaseStamina();
//
//                 if (_staminaController.sprintStamina <= 0.01f)
//                 {
//                     canRun = false; 
//                 }
//             }
//             
//         }
//     }
// }

using Runtime.Data.ValueObjects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        public CharacterController characterController;
        public float moveSpeed = 5f;
        public float gravity = -9.81f;
        public Transform groundCheck;
        public LayerMask groundMask;

        [ShowInInspector] private PlayerMovementData _data;
        
        private bool isGrounded;
        private Vector3 velocity;
        public bool canMove = true;

        private void Start()
        {
            characterController = GetComponent<CharacterController>();
        }
        internal void SetData(PlayerMovementData movementData)
         {
             _data = movementData;
         }
        private void Update()
        {
            if (!canMove)
                return;

            isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            Vector3 move = transform.right * x + transform.forward * z;
            
            characterController.Move(move * _data.ForwardSpeed * Time.deltaTime);
            
            velocity.y += gravity * Time.deltaTime;
            characterController.Move(velocity * Time.deltaTime);
        }
    }
}