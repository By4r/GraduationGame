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
        
        [SerializeField] private GameObject pressEtext;

        [ShowInInspector] private TaskData _taskData;

        internal void SetData(TaskData taskData)
        {
            _taskData = taskData;
        }

        private void Start()
        {
            //Invoke("PickUpPhone", 5f);
        }
       
        private void Update()
        {
            taskText.text = string.Format("Collect the garbages {0}/{1}", _taskData.garbageAmount, _taskData.maxGarbageAmount);
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
                        sleepController.Sleep();
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
            BringTaskTab();
            taskText.text = "Pick up the phone";
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
                _taskData.wateringAmount++;
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
            _taskData.garbageAmount += 1;
        }
    }
}
