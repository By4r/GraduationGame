﻿using System;
using System.Collections.Generic;
using Runtime.Controllers;
using Runtime.Controllers.Player;
using Runtime.Data.ValueObjects;
using Runtime.Managers;
using Sirenix.OdinInspector;
using UnityEngine;


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

        [ShowInInspector] private PlayerPickUpController _playerPickUpController;

        private WorkData _workData;

        [Header("TASK MANAGERS")] [SerializeField]
        private CheckHouseManager checkHouseManager;

        [SerializeField] private CheckOfficeManager checkOfficeManager;

        [SerializeField] private LetterManager letterManager;

        [SerializeField] private GoCarManager goCarManager;

        private void Start()
        {
            //_currentState = "PickUpPhone";

            _playerPickUpController = FindObjectOfType<PlayerPickUpController>();

            //_currentState = "CollectGarbage";

            _currentState = "GoCar";


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

            stateActions["Attic"] = Attic;

            stateActions["GoCar"] = GoCar;

            stateActions["HeatHouse"] = HeatHouse;

            stateActions["CheckUpstairs"] = CheckUpstairs;
        }

        private void Attic()
        {
            Ray raycast = playerPhysicsController.GetRaycast();
            float range = playerPhysicsController.range;

            if (Physics.Raycast(raycast, out RaycastHit hit, range))
            {
                if (hit.collider.CompareTag("Letter"))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        letterManager.GetLetterPrefab(0);
                    }
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_currentState == "SweepFloor" && other.CompareTag("SweepArea"))
            {
                Debug.Log("SWEEP AREA ENTER");
            }

            if (_currentState == "CheckHouse" && other.CompareTag("ParanormalEnter"))
            {
                Debug.Log("PARANORMAL ENTER TAG");

                checkHouseManager.ShowParanormal();
            }

            if (_currentState == "CheckHouse" && other.CompareTag("ParanormalExit"))
            {
                Debug.Log("PARANORMAL EXIT TAG");

                checkHouseManager.HideParanormal();
            }

            if (_currentState == "GoCar" && other.CompareTag("Car"))
            {
                Debug.Log("CAR TAG");
                Debug.Log("YOU SHOULD FULL THE TANK ");
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (_currentState == "SweepFloor" && other.CompareTag("SweepArea"))
            {
                Debug.Log("SWEEP AREA EXIT");

                //_playerPickUpController.InSweepArea(false);
            }
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
            Debug.Log("GO CAR STATE!");

            Ray raycast = playerPhysicsController.GetRaycast();
            float range = playerPhysicsController.range;

            if (Physics.Raycast(raycast, out RaycastHit hit, range))
            {
                if (hit.collider.CompareTag("Letter"))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        letterManager.GetLetterPrefab(1);
                    }
                }
            }
        }

        private void CheckOffice()
        {
            Debug.Log("CHECK OFFICE STATE!");

            Ray raycast = playerPhysicsController.GetRaycast();
            float range = playerPhysicsController.range;

            if (Physics.Raycast(raycast, out RaycastHit hit, range))
            {
                if (hit.collider.CompareTag("Key"))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        checkOfficeManager.KeyReceived();
                    }
                }
            }
        }

        private void CheckCamera()
        {
            throw new NotImplementedException();
        }

        private void CheckHouse()
        {
            Debug.Log("CHECK HOUSE STATE!");
        }

        private void WateringFlowers()
        {
            Ray raycast = playerPhysicsController.GetRaycast();
            float range = playerPhysicsController.range;

            if (Physics.Raycast(raycast, out RaycastHit hit, range))
            {
                if (hit.collider.CompareTag("Flower"))
                {
                    //_playerPickUpController.WaterFlowers();
                }
            }
        }

        private void SweepFloor()
        {
            //_playerPickUpController.SweepFloor();

            Ray raycast = playerPhysicsController.GetRaycast();
            float range = playerPhysicsController.range;

            if (Physics.Raycast(raycast, out RaycastHit hit, range))
            {
                if (hit.collider.CompareTag("SweepArea"))
                {
                    //_playerPickUpController.SweepFloor();
                }
            }
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
                        //sleepController.Sleep();
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

            // if (Input.GetKeyDown(KeyCode.E))
            // {
            //     SetState("CollectGarbage");
            // }

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
                case "Attic":
                    return "Check the attic";
                case "GoCar":
                    return "Get in the car";
                case "HeatHouse":
                    return "Turn on the heating";
                case "CheckUpstairs":
                    return "Inspect the upstairs";
                case "CallPhone":
                    return "Call the phone";
                default:
                    return "";
            }
        }
    }
}