using System;
using System.Collections;
using Runtime.Data.ValueObjects;
using Runtime.Enums;
using Runtime.Managers;
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

        [SerializeField] private float range;
        [SerializeField] private GameObject pressEtext;

        [ShowInInspector] private TaskData _taskData;

        internal void SetData(TaskData taskData)
        {
            _taskData = taskData;
        }

        private void Start()
        {
            Invoke("PickUpPhone", 5f);
        }

        private void Update()
        {
            Ray raycast = new Ray(playerManager.playerEyes.transform.position,
                playerManager.playerEyes.transform.TransformDirection(Vector3.forward * range));

            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward * range), Color.green);

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
                taskText.text = "Water the flowers " + _taskData.wateringAmount + "/" + _taskData.maxWateringAmount;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("tasks"))
            {
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
    }
}
