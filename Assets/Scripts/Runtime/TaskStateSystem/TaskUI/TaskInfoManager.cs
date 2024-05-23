using Sirenix.OdinInspector;
using UnityEngine;

namespace Runtime.TaskStateSystem.TaskUI
{
    public class TaskInfoManager:MonoBehaviour
    {
        [SerializeField] private TaskInfoController taskInfoController;

        [ShowInInspector] private string _currentState;
        
        internal void SetStateForInfo(string stateName)
        {
            _currentState = stateName;
            taskInfoController.TaskInfo(GetTaskTextForState(stateName));
            
            ShowInfoTab();
        }

        private void ShowInfoTab()
        {
            taskInfoController.ShowTaskInfo();
        }

        internal void HideInfoTab()
        {
            taskInfoController.HideTaskInfo();
        }
        
        private string GetTaskTextForState(string state)
        {
            switch (state)
            {
                case "PickUpPhone":
                    return "Pick up the phone";
                case "CollectGarbage":
                    return $"Collect the garbage";
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
                    return "Inspect the Upstairs";
                default:
                    return "";
            }
        }
        
    }
}