using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class PlayerPickUpController : MonoBehaviour
    {
        [SerializeField] private PlayerPhysicsController playerPhysicsController;
<<<<<<< HEAD
=======
        [SerializeField] LayerMask layerMask;
        [SerializeField] private Rigidbody _rigidbody;
>>>>>>> origin/main
        [SerializeField] private Transform itemContainer;


        private Transform _currentPickedItem;

        private Animator animator;
        private ParticleSystem particleSystem;

        private bool ispickuped;

        private void Update()
        {
            Ray raycast = playerPhysicsController.GetRaycast();
            float range = playerPhysicsController.range;


            if (Physics.Raycast(raycast, out RaycastHit hit, range))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (hit.collider.CompareTag("Pickable"))
                    {
                        PlayerPickUp(hit);
                    }
                    else
                    {
                        Debug.Log("No Item");
                    }
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                LeaveItem();
            }
        }

        private void PlayerPickUp(RaycastHit hitInfo)
        {
            ToggleItemPick();

            hitInfo.rigidbody.useGravity = false;
            hitInfo.rigidbody.isKinematic = true;

            _currentPickedItem = hitInfo.transform;

<<<<<<< HEAD
            _currentPickedItem.SetParent(itemContainer);
            _currentPickedItem.localPosition = itemContainer.transform.localPosition;
            _currentPickedItem.localRotation = Quaternion.identity;
=======
                hit.transform.SetParent(itemContainer);
                hit.transform.localPosition = itemContainer.transform.localPosition;
                hit.transform.localRotation = Quaternion.identity;
                _rigidbody.useGravity = false;
                _rigidbody.isKinematic = true;
                
            }
            else
            {
                Debug.LogWarning("No Item");
            }
>>>>>>> origin/main
        }


        private void LeaveItem()
        {
<<<<<<< HEAD
            if (_currentPickedItem != null)
            {
                _currentPickedItem.SetParent(null);
=======
            if (transform.parent == itemContainer)
            {
                transform.SetParent(null);
                _rigidbody.useGravity = true;
                _rigidbody.isKinematic = false;
            }
        }
>>>>>>> origin/main

                Rigidbody rb = _currentPickedItem.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.useGravity = true;
                    rb.isKinematic = false;
                }

                _currentPickedItem = null;
                ToggleItemPick();
            }
        }
        
        private void ToggleItemPick()
        {
            ispickuped = !ispickuped;
        }
        
    }
}