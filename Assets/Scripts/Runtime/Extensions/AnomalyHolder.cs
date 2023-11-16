using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Extensions
{
    public struct AnomalyHolder
    {
        public List<GameObject> AnomalyList;

        public AnomalyHolder(List<GameObject> holders)
        {
            AnomalyList = holders;
        }
    }
}