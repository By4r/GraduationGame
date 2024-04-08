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

        private ItemPickUpController itemPickUpController;

        private void Start()
        {
            _playerManager = FindObjectOfType<PlayerManager>();
            playerLook = _playerManager.playerEyes;
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
            if (Physics.Raycast(playerLook.transform.position, playerLook.transform.TransformDirection(Vector3.forward),
                    out RaycastHit hitInfo, 20f,_layerMask,QueryTriggerInteraction.Collide))
            {
                Debug.LogWarning("Pickable Item");
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hitInfo.distance,
                    Color.green);

                
                itemPickUpController = hitInfo.collider.GetComponent<ItemPickUpController>();
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