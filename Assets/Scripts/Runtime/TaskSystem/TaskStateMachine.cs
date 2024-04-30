using System;
using System.Collections.Generic;
using Runtime.Controllers;
using Runtime.Controllers.Player;
using Runtime.Controllers.Task_Tab;
using Runtime.Data.ValueObjects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Runtime.TaskSystem
{
    public class TaskStateMachine : MonoBehaviour
    {
        [ShowInInspector] private Dictionary<string, Action> stateActions = new Dictionary<string, Action>();
        
        private string currentState = "PickUpPhone";

        private TaskController controller;

        [SerializeField] private PlayerPhysicsController playerPhysicsController;

        [SerializeField] private SleepController sleepController;

        [SerializeField] private AudioSource audioSource;
        
        private WorkData _workData;


        private void Start()
        {
            DefineState();
        }

        private void SetData(WorkData workData)
        {
            _workData = workData;
        }


        private void DefineState()
        {
            stateActions["PickUpPhone"] = PickUpPhone;

            stateActions["CollectGarbage"] = CollectGarbage;

            stateActions["GoSleep"] = GoSleep;
            
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

                        RemoveTaskTab();
                    }
                }

                SetState("CollectGarbage");
            }
        }


        public void Update()
        {
            stateActions[currentState]?.Invoke();
        }

        public void SetState(string newState)
        {
            currentState = newState;
            Debug.Log("Current State: " + currentState);
            controller.TaskInfo(GetTaskTextForState(newState));
        }

        private void RemoveTaskTab()
        {
            // Görev sekmesini kaldırma işlemi
        }

        private string GetTaskTextForState(string state)
        {
            switch (state)
            {
                case "PickUpPhone":
                    return "Pick up the phone";
                case "SweepFloor":
                    return "Sweep the floor";
                case "CollectGarbage":
                    return $"Collect the garbage (/{_workData.MaxGarbageAmount})";
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
                case "CheckFloor":
                    return "Inspect the floor";
                default:
                    return "";
            }
        }

    }
}