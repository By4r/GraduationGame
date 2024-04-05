using System;
using System.Collections;
using Runtime.Enums;
using TMPro;
using UnityEngine;

namespace Runtime.Controllers.Task_Tab
{
    public class TaskTabController: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI taskText;
        [SerializeField] private Animator taskTabAnim;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("tasks"))
            {
                taskTabAnim.SetTrigger(TaskAnimationStates.BringTask.ToString());
                Debug.Log("task debug");
                taskTabAnim.Play("taskanim", -1, 0f);
                taskText.text = "Collect flowers 0/10";
            }
        }

        private void Start()
        {
            Invoke("PickUpPhone",5f);
        }
        

        private void PickUpPhone()
        {
            taskTabAnim.SetTrigger(TaskAnimationStates.BringTask.ToString());
            taskText.text = "Pick up the phone";
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("tasks"))
            {
                
                taskTabAnim.SetTrigger(TaskAnimationStates.RemoveTask.ToString());
            }

        }
    }
}