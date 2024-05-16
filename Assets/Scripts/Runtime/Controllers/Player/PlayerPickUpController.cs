using System;
using Runtime.Controllers.Item;
using Runtime.Managers;
using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class PlayerPickUpController : MonoBehaviour
    {
        [SerializeField] private PlayerPhysicsController playerPhysicsController;
        [SerializeField] LayerMask layerMask;
        private ItemPickUpController itemPickUpController;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Transform itemContainer;


        private Animator animator;
        private ParticleSystem particleSystem;

        private bool ispickuped;
        private float wateringTime;
        private float maxWateringTime = 3f;
        private bool isWatering;
        private bool hasWatered;
        private bool pickedUp; //oyuncunun elinde tool var ise elindekini bırakmadan yeni tool alamasın

        private bool inSweepArea;

        void Start()
        {
            particleSystem = GetComponentInChildren<ParticleSystem>();
            animator = GetComponentInChildren<Animator>();
            particleSystem.Stop();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PlayerPickUp();
            }

            if (Input.GetMouseButtonDown(1))
            {
                LeaveItem();
            }
            //PlayerWateringFlowers();
            //SweepFloor();
        }

        public void PlayerPickUp()
        {
            Ray raycast = playerPhysicsController.GetRaycast();
            float range = playerPhysicsController.range;


            if (Physics.Raycast(raycast, out RaycastHit hit, range, layerMask))
            {
                Debug.LogWarning("Pickable Item");
                TogglePickup();

                hit.transform.SetParent(itemContainer);
                hit.transform.localPosition = itemContainer.transform.localPosition;
                hit.transform.localRotation = Quaternion.identity;
                _rigidbody.useGravity = false;
                _rigidbody.isKinematic = true;


                itemPickUpController = hit.collider.GetComponent<ItemPickUpController>();
                if (itemPickUpController != null)
                {
                    itemPickUpController.Pickup();
                }
            }
            else
            {
                Debug.LogWarning("No Item");
            }
        }

        private void LeaveItem()
        {
            transform.SetParent(null);
            _rigidbody.useGravity = true;
            _rigidbody.isKinematic = false;
        }

        public void TogglePickup()
        {
            ispickuped = !ispickuped;
        }

        internal void SweepFloor()
        {
            // if (Input.GetMouseButtonDown(0))
            // {
            //     Debug.Log("Sweeping!");
            //     animator.SetTrigger("sweepFloor");
            // }
            // else
            // {
            //     animator.ResetTrigger("sweepFloor");
            // }
            
            animator.SetTrigger("sweepFloor");

        }

        internal void InSweepArea(bool state)
        {
            inSweepArea = state;
        }

        internal void WaterFlowers()
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
                    Debug.Log("Watering Done!");
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