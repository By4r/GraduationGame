using System;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Data.ValueObjects
{
    [Serializable]
    public class AnomalyData
    {
        public List<GameObject> SpawnReferences = new List<GameObject>();
    }
}