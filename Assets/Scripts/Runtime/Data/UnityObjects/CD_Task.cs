using Runtime.Data.ValueObjects;
using UnityEngine;

namespace Runtime.Data.UnityObjects
{
    [CreateAssetMenu(fileName = "CD_Task", menuName = "GraduationGame/CD_Task", order = 1)]
    public class CD_Task : ScriptableObject
    {
        public TaskDatas TaskData;
    }
}