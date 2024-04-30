using Runtime.Controllers.Task_Tab;
using Runtime.Data.UnityObjects;
using Runtime.Data.ValueObjects;
using Runtime.Signals;
using UnityEngine;
using UnityEngine.Serialization;

namespace Runtime.Managers
{
    public class TaskTabManager:MonoBehaviour
    {
        //[FormerlySerializedAs("taskTabController")] [SerializeField] private TaskController taskController;
        //private TaskDatas _taskData;
        
        
        private void Awake()
        {
            //_taskData = GetTaskData();
            SendDataToControllers();
        }
        private void SendDataToControllers()
        {
            //taskController.SetData(_taskData.workData);
        }

        // private TaskDatas GetTaskData()
        // {
        //     return Resources.Load<CD_Task>("Data/CD_Task").TaskData;
        // }
        
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            TaskSignals.Instance.onCollectGarbage += OnCollectGarbage;
        }

        private void OnCollectGarbage()
        {
            //taskController.IncreaseGarbageAmount();
        }

        private void UnSubscribeEvents()
        {
            TaskSignals.Instance.onCollectGarbage -= OnCollectGarbage;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
        
        
    }
}