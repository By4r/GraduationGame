using Runtime.Controllers.Player;
using Runtime.Controllers.Task_Tab;
using Runtime.Signals;
using UnityEngine;
using UnityEngine.Serialization;

namespace Runtime.Controllers.Item
{
    public class ItemCollectController : MonoBehaviour
    {
        [SerializeField] private PlayerPhysicsController playerPhysicsController;
        //[FormerlySerializedAs("taskTabController")] [SerializeField] private TaskController taskController;
        [SerializeField] LayerMask layerMask;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                CollectUp();
            }
        }


        private void CollectUp()
        {
            Ray raycast = playerPhysicsController.GetRaycast();
            float range = playerPhysicsController.range;

            if (Physics.Raycast(raycast, out RaycastHit hit, range, layerMask)) //&& hit.collider.CompareTag("Broom"))
            {
                //Debug.LogWarning("Collectable Item");
                //TaskSignals.Instance.onCollectGarbage?.Invoke();

                //TaskSignals.Instance.onCollectGarbage?.Invoke();
                

                //Destroy(hit.collider.gameObject);
                
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