using Runtime.Controllers;
using Runtime.Controllers.Player;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Runtime.TaskSystem
{
    public class TaskController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI taskInfo;

        [SerializeField] private PlayerPhysicsController playerPhysicsController;

        [SerializeField] private SleepController sleepController;
        
        [SerializeField] private AudioSource audioSource;
        
        [SerializeField] private AudioClip audioClip;



        internal void Sleep()
        {
            sleepController.Sleep();   
        }

        internal void TaskInfo(string info)
        {
            taskInfo.text = info;
        }
    }
}