﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Data.ValueObjects
{
    [Serializable]
    public class UniqueAnomalyData
    {
        public List<GameObject> SpawnReferences = new List<GameObject>();
    }
}