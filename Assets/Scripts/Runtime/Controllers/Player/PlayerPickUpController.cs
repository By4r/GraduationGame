using System;
using Runtime.Controllers.Item;
using Runtime.Managers;
using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class PlayerPickUpController: MonoBehaviour
    {
        
        [SerializeField] private PlayerPhysicsController playerPhysicsController;
        [SerializeField] LayerMask layerMask;
        private ItemPickUpController itemPickUpController;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Transform itemContainer;
        
        private bool pickedUp; //oyuncunun elinde tool var ise elindekini bırakmadan yeni tool alamasın
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PlayerPickUp();
            }
            
            if (Input.GetMouseButtonDown(1))
            {
                DropItem();
            }
        }
        
        private void DropItem()
        {
            transform.SetParent(null);
            _rigidbody.useGravity = true;
            _rigidbody.isKinematic = false;
        }
        public void PlayerPickUp()
        {
            Ray raycast = playerPhysicsController.GetRaycast();
            float range = playerPhysicsController.range;

            if (Physics.Raycast(raycast, out RaycastHit hit, range,layerMask) )//&& hit.collider.CompareTag("Broom"))
            {
                Debug.LogWarning("Pickable Item");

                // Nesneyi itemContainer'ın pozisyonuna ve rotasyonuna getir
                hit.transform.SetParent(itemContainer);
                hit.transform.localPosition = itemContainer.transform.localPosition;
                hit.transform.localRotation = Quaternion.identity;
                _rigidbody.useGravity = false;
                _rigidbody.isKinematic = true;
            
               
                
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