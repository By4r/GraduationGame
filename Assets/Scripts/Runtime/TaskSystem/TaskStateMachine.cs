using System;
using System.Collections.Generic;
using Runtime.Controllers;
using Runtime.Controllers.Player;
using Runtime.Controllers.Task_Tab;
using Runtime.Data.ValueObjects;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.iOS;
using UnityEngine.Serialization;

namespace Runtime.TaskSystem
{
    public class TaskStateMachine : MonoBehaviour
    {
        [ShowInInspector] private Dictionary<string, Action> stateActions = new Dictionary<string, Action>();

        [ShowInInspector] private string _currentState;

        [SerializeField] private TaskController taskController;

        [SerializeField] private PlayerPhysicsController playerPhysicsController;

        [SerializeField] private SleepController sleepController;

        [SerializeField] private AudioSource audioSource;

        private WorkData _workData;


        private void Start()
        {
            //_currentState = "PickUpPhone";
            _currentState = "CollectGarbage";

            DefineState();
        }

        // private void SetData(WorkData workData)
        // {
        //     _workData = workData;
        // }


        public void Update()
        {
            stateActions[_currentState]?.Invoke();
        }

        private void DefineState()
        {
            stateActions["PickUpPhone"] = PickUpPhone;

            stateActions["CollectGarbage"] = CollectGarbage;

            stateActions["SweepFloor"] = SweepFloor;

            stateActions["WateringFlowers"] = WateringFlowers;

            stateActions["GoSleep"] = GoSleep;

            stateActions["CheckHouse"] = CheckHouse;

            stateActions["CheckCamera"] = CheckCamera;

            stateActions["CheckOffice"] = CheckOffice;

            stateActions["GoCar"] = GoCar;

            stateActions["HeatHouse"] = HeatHouse;

            stateActions["CheckUpstairs"] = CheckUpstairs;
        }


        private void CheckUpstairs()
        {
            throw new NotImplementedException();
        }

        private void HeatHouse()
        {
            throw new NotImplementedException();
        }

        private void GoCar()
        {
            throw new NotImplementedException();
        }

        private void CheckOffice()
        {
            throw new NotImplementedException();
        }

        private void CheckCamera()
        {
            throw new NotImplementedException();
        }

        private void CheckHouse()
        {
            throw new NotImplementedException();
        }

        private void WateringFlowers()
        {
            Ray raycast = playerPhysicsController.GetRaycast();
            float range = playerPhysicsController.range;

            if (Physics.Raycast(raycast, out RaycastHit hit, range))
            {
                if (hit.collider.CompareTag("Flower"))
                {
                    Debug.Log("flower watering triggered");
                }
            }
        }

        private void SweepFloor()
        {
            throw new NotImplementedException();
        }

        private void GoSleep()
        {
            Debug.Log("GO SLEEP STATE");

            Ray raycast = playerPhysicsController.GetRaycast();
            float range = playerPhysicsController.range;

            if (Physics.Raycast(raycast, out RaycastHit hit, range))
            {
                if (hit.collider.CompareTag("Bed"))
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        sleepController.Sleep();
                    }
                }
            }
        }

        private void CollectGarbage()
        {
            Debug.Log("COLLECT GARBAGE STATE");
            
            Ray raycast = playerPhysicsController.GetRaycast();
            float range = playerPhysicsController.range;

            if (Physics.Raycast(raycast, out RaycastHit hit, range))
            {
                if (hit.collider.CompareTag("Collectable"))
                {
                    Debug.LogWarning("HIT COLLECTABLE");
                    
                    
                    if (Input.GetMouseButtonDown(0))
                    {
                        Destroy(hit.collider.gameObject);
                    }
                }
            }
            
            // if (controller.GarbageAmount >= controller.MaxGarbageAmount)
            // {
            //     SetState("GoSleep");
            // }
        }

        private void PickUpPhone()
        {
            Ray raycast = playerPhysicsController.GetRaycast();
            float range = playerPhysicsController.range;

            Debug.LogWarning("PICK UP PHONE STATE");

            if (Input.GetKeyDown(KeyCode.E))
            {
                SetState("CollectGarbage");
            }

            if (Physics.Raycast(raycast, out RaycastHit hit, range))
            {
                if (hit.collider.CompareTag("Phone"))
                {
                    //pressEtext.SetActive(true);

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Debug.Log("talking audio started");

                        // Check if an audio clip is assigned
                        if (audioSource.clip != null)
                        {
                            // Play the assigned audio clip
                            //audioSource.PlayOneShot(audioClip);
                            //subtitleManager.StartSpeech();
                            Invoke("FinishPhone", 24f);
                        }
                        else
                        {
                            Debug.LogWarning("No audio clip assigned to audioSourceObject.");
                        }
                    }
                }

                SetState("CollectGarbage");
            }
        }


        public void SetState(string newState)
        {
            _currentState = newState;
            Debug.Log("Current State: " + _currentState);
            taskController.TaskInfo(GetTaskTextForState(newState));
        }

        private string GetTaskTextForState(string state)
        {
            switch (state)
            {
                case "PickUpPhone":
                    return "Pick up the phone";
                case "CollectGarbage":
                    return $"Collect the garbage (/{_workData.MaxGarbageAmount})";
                case "SweepFloor":
                    return "Sweep the floor";
                case "WateringFlowers":
                    return "Water the flowers";
                case "GoSleep":
                    return "Go to sleep";
                case "CheckHouse":
                    return "Check out the house";
                case "CheckCamera":
                    return "Check the security camera";
                case "CheckOffice":
                    return "Check the office";
                case "GoCar":
                    return "Get in the car";
                case "HeatHouse":
                    return "Turn on the heating";
                case "CheckUpstairs":
                    return "Inspect the Upstairs";
                default:
                    return "";
            }
        }
    }
}