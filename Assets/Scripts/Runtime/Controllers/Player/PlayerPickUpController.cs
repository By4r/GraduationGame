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
        
        
        private Animator animator;
        private ParticleSystem particleSystem;
        
        private float wateringTime = 0f;
        private float maxWateringTime = 3f;
        private bool isWatering = false;
        private bool hasWatered = false;
        private bool pickedUp; //oyuncunun elinde tool var ise elindekini bırakmadan yeni tool alamasın
        
        void Start()
        {
            particleSystem = GetComponentInChildren<ParticleSystem>();
            animator = GetComponentInChildren<Animator>();
        }
        
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
            PlayerWateringFlowers();
            PlayerSweepFloor();
           
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
            
            if (Physics.Raycast(raycast, out RaycastHit hit, range,layerMask) )
            {
                Debug.LogWarning("Pickable Item");


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

            }
        }

        private void PlayerSweepFloor()
        {
            if (Input.GetMouseButton(0))
            {
                animator.SetTrigger("sweepFloor");
            }
            else
            {
                animator.ResetTrigger("sweepFloor");
            }
            
        }

        private void PlayerWateringFlowers()
        {
            if (Input.GetMouseButtonDown(0))
            {
                isWatering = true;
                animator.SetTrigger("upCan"); 
                wateringTime = 0f;
                hasWatered = false; 
                if (particleSystem != null)
                {
                    particleSystem.Play();
                }
                else return;
            }

            if (isWatering)
            {
                wateringTime += Time.deltaTime;
                if (wateringTime >= maxWateringTime && !hasWatered)
                {
                    Debug.Log("çiçek sulandı");
                    hasWatered = true; 
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                animator.SetTrigger("downCan");
                isWatering = false;
                if (particleSystem != null)
                {
                    particleSystem.Stop();
                }
                



            }
        }
     

       
    }
}