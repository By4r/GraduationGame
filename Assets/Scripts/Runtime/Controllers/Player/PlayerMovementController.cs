using Runtime.Data.ValueObjects;
using Runtime.Keys;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        
        [SerializeField] public CharacterController characterController;
        #endregion

        #region Private Variables

        [ShowInInspector] private PlayerMovementData _data;

        #endregion

        #endregion

        internal void SetData(PlayerMovementData data)
        {
            _data = data;
        }

        private void FixedUpdate()
        {
            MovePlayer();
            SprintPlayer();
        }

        private void MovePlayer()
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;
            characterController.Move(move * _data.ForwardSpeed * Time.deltaTime);
        }

        private void SprintPlayer()
        {
            if (Input.GetKey("left shift"))
            {
                Vector3 run = transform.forward;
                characterController.Move(run * _data.SprintSpeed * Time.deltaTime);

            }

        }
    }
}