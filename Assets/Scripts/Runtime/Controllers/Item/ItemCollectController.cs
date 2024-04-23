using Runtime.Controllers.Player;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.Item
{
    public class ItemCollectController: MonoBehaviour
    {
        
        [SerializeField] private PlayerPhysicsController playerPhysicsController;
        [SerializeField] LayerMask layerMask;
        
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                CollectUp();
            }
        }
        
        
        public void CollectUp()
        {
            Ray raycast = playerPhysicsController.GetRaycast();
            float range = playerPhysicsController.range;

            if (Physics.Raycast(raycast, out RaycastHit hit, range,layerMask) )//&& hit.collider.CompareTag("Broom"))
            {
                Debug.LogWarning("Collectable Item");
                
                Destroy(hit.collider.gameObject);
                TaskSignals.Instance.onCollectGarbage?.Invoke();
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