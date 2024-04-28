using System;
using Sirenix.OdinInspector;

namespace Runtime.Data.ValueObjects
{
    [Serializable]
    public struct TaskDatas
    {
        public TaskData taskData;
    }

    [Serializable]
    public struct TaskData
    {
        [Title("Max Amount")]
        public int maxSweepAmount;
        public int maxGarbageAmount;
        public int maxWateringAmount;
    }
}