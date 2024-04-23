using System;
using UnityEngine;

namespace Runtime.Data.ValueObjects
{
    [Serializable]
    public class ItemData
    {
        public int id;
        public string itemName;
        public int value;
        public Sprite icon;
    }
}