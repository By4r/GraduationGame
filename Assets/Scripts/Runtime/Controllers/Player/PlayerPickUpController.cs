using System;
using Runtime.Controllers.Item;
using Runtime.Managers;
using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class PlayerPickUpController: MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private GameObject playerLook;
        [SerializeField] private PlayerManager _playerManager;
        [SerializeField] private PlayerPhysicsController playerPhysicsController;
        
        private ItemPickUpController itemPickUpController;

        private void Start()
        {
            _playerManager = FindObjectOfType<PlayerManager>();
            playerLook = playerPhysicsController.playerEyes;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PlayerPickUp();
            }
        }

        public void PlayerPickUp()
        {
            Ray raycast = playerPhysicsController.GetRaycast();
            float range = playerPhysicsController.range;
            
            if (Physics.Raycast(raycast, out RaycastHit hit, range))
            {
                Debug.LogWarning("Pickable Item");
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance,
                    Color.green);

                
                itemPickUpController = hit.collider.GetComponent<ItemPickUpController>();
                if(itemPickUpController != null)
                {
                    itemPickUpController.Pickup();
                }
            }
            else
            {
                Debug.LogWarning("No Item");
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 20f,
                    Color.red);

            }
        }
    }
}