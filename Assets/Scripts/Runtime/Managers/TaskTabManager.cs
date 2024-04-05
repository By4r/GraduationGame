using Runtime.Controllers.Task_Tab;
using Runtime.Data.UnityObjects;
using Runtime.Data.ValueObjects;
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
        
        
    }
}