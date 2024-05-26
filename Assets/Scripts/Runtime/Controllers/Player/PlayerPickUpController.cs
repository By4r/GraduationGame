using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class PlayerPickUpController : MonoBehaviour
    {
        [SerializeField] private PlayerPhysicsController playerPhysicsController;
        [SerializeField] private Transform itemContainer;

        private Transform _currentPickedItem;

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

        internal void PlayerPickUp(RaycastHit hitInfo)
        {
            ToggleItemPick();

            hitInfo.rigidbody.useGravity = false;
            hitInfo.rigidbody.isKinematic = true;

            _currentPickedItem = hitInfo.transform;
            
            _currentPickedItem.SetParent(itemContainer);
            _currentPickedItem.localPosition = itemContainer.transform.localPosition;
            _currentPickedItem.localRotation = Quaternion.identity;
        }
        
        internal void LeaveItem()
        {
            if (_currentPickedItem != null)
            {
                _currentPickedItem.SetParent(null);

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