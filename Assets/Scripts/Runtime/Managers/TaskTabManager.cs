using Runtime.Controllers.Task_Tab;
using Runtime.Data.UnityObjects;
using Runtime.Data.ValueObjects;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class TaskTabManager:MonoBehaviour
    {
        [SerializeField] private TaskTabController taskTabController;
        private TaskDatas _taskData;
        
        
        private void Awake()
        {
            _taskData = GetTaskData();
            SendDataToControllers();
        }
        private void SendDataToControllers()
        {
            taskTabController.SetData(_taskData.taskData);
        }

        private TaskDatas GetTaskData()
        {
            return Resources.Load<CD_Task>("Data/CD_Task").TaskData;
        }
        
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
            taskTabController.IncreaseGarbageAmount();
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