﻿using Runtime.Data.ValueObjects;
using UnityEngine;

namespace Runtime.Data.UnityObjects
{
    [CreateAssetMenu(fileName = "CD_Player", menuName = "Picker3D/CD_Player", order = 2)]
    public class CD_Player : ScriptableObject
    {
        public PlayerData Data;
    }
}