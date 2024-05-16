using System;
using System.Collections;
using Runtime.Controllers.Player;
using Runtime.Data.ValueObjects;
using Runtime.Enums;
using Runtime.Managers;
using Runtime.Signals;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;


namespace Runtime.Controllers.Task_Tab
{
    public class TaskTabController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI taskText;
        [SerializeField] private Animator taskTabAnim;
        [SerializeField] private PlayerManager playerManager;
        [SerializeField] private PlayerPhysicsController playerPhysicsController;
        [SerializeField] private SleepController sleepController;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip audioClip;
        [SerializeField] private SubtitleManager subtitleManager;
        [SerializeField] private Animator animator;
        [SerializeField] private GameObject pressEtext;

        [ShowInInspector] private WorkData _workData;

        private int _garbageAmount;
        private int _wateringAmount;

        private bool _isSleepDone;

        private bool _isGarbageCollectTask = false;

        internal void SetData(WorkData workData)
        {
            _workData = workData;
        }

        private void Start()
        {
            Invoke("PickUpPhone", 5f);
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            TaskSignals.Instance.onSleepDone += OnSleepDone;
        }

        private void OnSleepDone(bool state)
        {
            _isSleepDone = state;

            if (_isSleepDone)
            {
                Debug.LogWarning("Sleep Done ! 'At Midnight!");

                TaskState(3);

                RemoveTaskTab();
            }
        }

        private void UnSubscribeEvents()
        {
            TaskSignals.Instance.onSleepDone -= OnSleepDone;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void Update()
        {
            // if (_isGarbageCollectTask)
            // {
            //     taskText.text = string.Format("Collect the garbages {0}/{1}", _garbageAmount,
            //         _workData.maxGarbageAmount);
            // }

            Ray raycast = playerPhysicsController.GetRaycast();
            float range = playerPhysicsController.range;


            if (Physics.Raycast(raycast, out RaycastHit hit, range))
            {
                if (hit.collider.CompareTag("Phone"))
                {
                    pressEtext.SetActive(true);

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

                if (hit.collider.CompareTag("Collectable"))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        IncreaseGarbageAmount();
                        Destroy(hit.collider.gameObject);
                    }
                }

                if (hit.collider.CompareTag("Bed"))
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        //sleepController.Sleep();
                    }
                }
            }
            else
            {
                pressEtext.SetActive(false);
            }
        }

        private void PickUpPhone()
        {
            taskText.text = "Pick up the phone";
            BringTaskTab();
            Debug.LogWarning("PICK UP PHONE METHOD WORKED!");
        }

        private void FinishPhone()
        {
            TaskState(1);
        }

        private void FinishGarbageWork()
        {
            TaskState(2);
            _isGarbageCollectTask = false;
        }

        private void SweepFloor()
        {
        }

        private void WateringFlowers()
        {
        }

        private void CollectGarbage()
        {
            _isGarbageCollectTask = true;
            BringTaskTab();
            // taskText.text = string.Format("Collect the garbages {0}/{1}", _garbageAmount,
            //     _workData.maxGarbageAmount);
            Debug.LogWarning("Collect Garbage Task!");
        }

        private void GoSleep()
        {
            BringTaskTab();
            taskText.text = "Go to sleep";
            Debug.LogWarning("Go Sleep Task!");
        }

        private void CheckHouse()
        {
            BringTaskTab();
            taskText.text = "Check Out House";
            Debug.LogWarning("Check Out House Task!");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("tasks"))
            {
                BringTaskTab();
                //taskText.text = string.Format("Collect the garbages {0}/{1}", _taskData.garbageAmount, _taskData.maxGarbageAmount);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("tasks"))
            {
                _wateringAmount++;
                RemoveTaskTab();
            }
        }

        private void BringTaskTab()
        {
            if (!taskTabAnim.GetCurrentAnimatorStateInfo(0).IsName("BringTask"))
            {
                taskTabAnim.SetTrigger(TaskAnimationStates.BringTask.ToString());
            }
        }

        private void RemoveTaskTab()
        {
            if (!taskTabAnim.GetCurrentAnimatorStateInfo(0).IsName("RemoveTask"))
            {
                taskTabAnim.SetTrigger(TaskAnimationStates.RemoveTask.ToString());
            }
        }

        internal void IncreaseGarbageAmount()
        {
            _garbageAmount += 1;

            // if (_garbageAmount >= _workData.maxGarbageAmount)
            // {
            //     //RemoveTaskTab();
            //
            //     FinishGarbageWork();
            //     Debug.LogWarning("Finished Garbage Task!");
            // }
        }

        private void TaskState(int stateIndex)
        {
            switch (stateIndex)
            {
                case 0:
                    PickUpPhone();
                    break;
                case 1:
                    SweepFloor();
                    break;
                case 2:
                    CollectGarbage();
                    break;
                case 3:
                    WateringFlowers();
                    break;
                case 4:
                    GoSleep();
                    break;
                case 5:
                    CheckHouse();
                    break;
                default:
                    break;
            }
        }
    }
}