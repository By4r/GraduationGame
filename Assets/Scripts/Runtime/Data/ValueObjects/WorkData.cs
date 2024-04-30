using System;
using Unity.Mathematics;

namespace Runtime.Data.ValueObjects
{
    [Serializable]
    public struct WorkData
    {
        public int MaxGarbageAmount;
        public int MaxSweepAmount;
        public int MaxWateringAmount;

        public WorkData(int maxGarbageAmount, int maxSweepAmount, int maxWateringAmount)
        {
            MaxGarbageAmount = maxGarbageAmount;
            MaxSweepAmount = maxSweepAmount;
            MaxWateringAmount = maxWateringAmount;
        }
    }
}

