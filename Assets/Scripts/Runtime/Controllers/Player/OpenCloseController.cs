using System;
using System.Collections.Generic;
using UnityEngine;
using Runtime.Enums;

namespace Runtime.Controllers.Player
{
    public class OpenCloseController : MonoBehaviour
    {

        [SerializeField] private List<Animator> _animators;
        [SerializeField] private PlayerPhysicsController playerPhysicsController;
        
        private OpenClose openCloseEnum; // Enumu kullanacak değişken
        private bool windowOpen;
        private bool doorOpen;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                OpenClose interactableType;
                GetInteractable(out interactableType);
                ToggleInteractable(interactableType);
            }
        }

        private void GetInteractable(out OpenClose interactableType)
        {
            interactableType = OpenClose.None;
            Ray raycast = playerPhysicsController.GetRaycast();
            float range = playerPhysicsController.range;
            RaycastHit hit;

            if (Physics.Raycast(raycast, out hit, range))
            {
                if (hit.collider.CompareTag("Door"))
                {
                    interactableType = OpenClose.Door;
                }

                if (hit.collider.CompareTag("Window"))
                {
                    interactableType = OpenClose.Window;
                }
            }
        }
        
        private void ToggleInteractable(OpenClose interactableType)
        {
            switch (interactableType)
            {
                case OpenClose.Door:
                    doorOpen = !doorOpen;
                    _animators[0].SetTrigger(doorOpen ? "DoorOpen" : "DoorClose");
                    break;
                
                case OpenClose.Window:
                    windowOpen = !windowOpen;
                    _animators[1].SetTrigger(windowOpen ? "WindowOpen" : "WindowClose");
                    break;
            }
        }
    }
    }
