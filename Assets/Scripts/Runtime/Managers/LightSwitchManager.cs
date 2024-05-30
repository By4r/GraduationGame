using System;
using Runtime.Controllers.Item_Inventory;
using Runtime.Controllers.Player;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Runtime.Managers
{
    public class LightSwitchManager : MonoBehaviour
    {
        [SerializeField] private PlayerPhysicsController physicsController;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Ray raycast = physicsController.GetRaycast();
                float range = physicsController.range;


                if (Physics.Raycast(raycast, out RaycastHit hit, range))
                {
                    LightSwitch lightSwitch = hit.collider.GetComponent<LightSwitch>();

                    if (hit.collider.CompareTag("LightSwitch"))
                    {
                        if (lightSwitch != null)
                        {
                            lightSwitch.ToggleObject();
                            
                        }else
                        {
                            Debug.LogWarning("Controlled object is not assigned in the LightSwitch component.");
                        }
                        
                    }
                }
            }
        }
    }
}