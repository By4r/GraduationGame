using Runtime.Data.ValueObjects;
using UnityEngine;


namespace Runtime.Data.UnityObjects
{
    [CreateAssetMenu(fileName = "CD_Work", menuName = "GraduationGame/CD_Work", order = 1)]
    public class CD_Work : ScriptableObject
    {
        public WorkData workData;
    }
}